using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShopperMovement : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _cofe;
    [SerializeField] private Transform _botle;
    [SerializeField] private Transform _packet;
    private LevelControl _levelControl;
    private float _movementSpeed = 25f;
    private float _rotationSpeed = 5f;
    private bool _isMove = false;
    private bool _isPaket = false;
    private bool _isBotle = false;
    private bool _isCofe = false;
    private List<Vector3> _path = new List<Vector3>();
    private int _curIndex = 0;
    private Vector3 _target;
    private Rigidbody _rb;
    private float _stoppingDistance = 0.2f;
    private Transform _body;
    private Animator _anim;
    private ShopPointControl _currentShopPoint;

    public bool IsPacket { get { return _isPaket; } }
    public bool IsBotle {  get { return _isBotle; } }
    public bool IsCofe { get { return _isCofe; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _body = transform.GetChild(0);
        _anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _cofe.gameObject.SetActive(false);
        _botle.gameObject.SetActive(false);
        _packet.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMove)
        {
            // ѕровер€ем, достигли ли мы текущей точки
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            Vector2 tg = new Vector2(_target.x, _target.z);
            //if (Vector3.Distance(transform.position, target) < stoppingDistance)
            if (Vector3.Distance(pos, tg) < _stoppingDistance)
            {
                NextPoint();
            }
            else
            {
                // ѕоворачиваем в сторону следующей точки
                LookAtWaypoint();

                // ѕеремещаем врага к текущей точке
                MoveTowardsWaypoint();
            }
        }
    }
    private void LookAtWaypoint()
    {
        // ѕоворачиваем врага в сторону следующей точки
        Vector3 dir = _target - transform.position; dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        _body.transform.rotation = Quaternion.Slerp(_body.transform.rotation, lookRot, _rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsWaypoint()
    {
        // ѕеремещаем врага к текущей точке
        Vector3 dir = _target - transform.position; dir.y = 0f;
        _rb.MovePosition(transform.position + dir.normalized * _movementSpeed * Time.deltaTime);
    }
    private void NextPoint()
    {
        if (_curIndex < _path.Count)
        {
            _target = _path[_curIndex];
            _curIndex++;
        }
        else
        {
            _curIndex = 0;
            _isMove = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FoodShelf"))
        {
            FoodShelf fs = other.gameObject.GetComponent<FoodShelf>();
            if ((fs != null) && (fs.GetFood()))
            {
                _anim.SetBool("IsWalk", false);
                _anim.SetBool("IsPacket", true);
                _isPaket = true;
                _packet.gameObject.SetActive(true);
            }
        }
        else if (other.CompareTag("ShopPoint"))
        {
            _currentShopPoint = other.GetComponent<ShopPointControl>();
            if (_currentShopPoint != null && _currentShopPoint.IsSale)
            {
                _isMove = false;
                Invoke("StartMovement", 7.2f);
                _anim.SetBool("IsWalk", false);
                _anim.SetBool("IsPay", true);
            }
        }
    }
    private void StartMovement()
    {
        _isMove = true;
        _anim.SetBool("IsPay", false);
       GameObject food = _currentShopPoint.GetFood();
        if (food != null)
        {
            Destroy(food);
            _levelControl.ChangeMany(_currentShopPoint.Price);
        }
        if (_currentShopPoint.TypeFood == 0)
        {
            _anim.SetBool("IsWalk", true);
            return;
        }
        if (_currentShopPoint.TypeFood == 1)
        {
            _anim.SetBool("IsPacket", true);
            _isPaket = true;
            _packet.gameObject.SetActive(true);
            /*if (food != null)
            {
                //food.transform.position = _rightHand.position;
                food.transform.parent = _rightHand;
                food.transform.localPosition = _rightHand.localPosition;
            }*/
        }
        else
        {
            _anim.SetBool("IsWalk", true);
            
            int num = Random.Range(0, 2);
            if (num == 0)
            {
                _isBotle = true;
                _isCofe = false;
                _botle.gameObject.SetActive(true);
                _cofe.gameObject.SetActive(false);
            }
            else
            {
                _isBotle = false;
                _isCofe = true;
                _botle.gameObject.SetActive(false);
                _cofe.gameObject.SetActive(true);
            }
            /*if (food != null)
            {
                //food.transform.position = _leftHand.position;
                food.transform.parent = _leftHand;
                food.transform.localPosition = _leftHand.localPosition;
            }*/
        }
    }

    public void SetPath(List<Vector3> pt, LevelControl lc)
    {
        _levelControl = lc;
        _path = pt;
        _isMove = true;
        _target = _path[_curIndex];
        _anim.SetBool("IsWalk", true);
    }
}
