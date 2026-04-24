using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KassirMovement : MonoBehaviour
{
    [SerializeField] private Transform _packet;
    private float _movementSpeed = 10f;
    private float _rotationSpeed = 45f;
    private bool _isMove = false;
    private bool _isPaket = false;
    private List<Vector3> _path = new List<Vector3>();
    private int _curIndex = 0;
    private Vector3 _target;
    private Rigidbody _rb;
    private float _stoppingDistance = 0.2f;
    private Transform _body;
    private Animator _anim;
    private GameObject _foodCreated = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _body = transform.GetChild(0);
        _anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HideFood();
        Vector3 point = transform.position;
        List<Vector3> pt = new List<Vector3>();
        pt.Add(point);
        point.z += 0.4f;
        pt.Add(point);
        point.x -= 2f;
        pt.Add(point);
        point.z -= 0.4f;
        pt.Add(point);
        point.x -= 2f;
        pt.Add(point);
        point.x += 2f;
        pt.Add(point);
        point.z += 0.4f;
        pt.Add(point);
        point.x += 2f;
        pt.Add(point);
        point.z -= 0.4f;
        pt.Add(point);
        SetPath(pt);
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
            _anim.SetBool("IsWalk", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (_isPaket == false)
            {
                Invoke("StartMovement", 3f);
                _isPaket = true;
                _foodCreated = other.gameObject;
                Invoke("HideCreatedFood", 3f);
            }
        }
        if (other.CompareTag("FoodShelf"))
        {
            if (_isPaket)
            {
                _isPaket = false;
                Invoke("HideFood", 0.5f);
                FoodShelf fs = other.gameObject.GetComponent<FoodShelf>();
                if (fs != null)
                {
                    fs.AddFood();
                }
            }
        }
    }

    private void HideCreatedFood()
    {
        if (_foodCreated != null)
        {
            _foodCreated.gameObject.SetActive(false);
            _foodCreated = null;
        }
    }

    private void HideFood()
    {
        _packet.gameObject.SetActive(false);
    }

    private void StartMovement()
    {
        _isMove = true;
        _target = _path[_curIndex];
        _anim.SetBool("IsWalk", true);
        _packet.gameObject.SetActive(true);
    }

    public void SetPath(List<Vector3> pt)
    {
        _path = pt;
        //_isMove = true;
        _target = _path[_curIndex];
        //_anim.SetBool("IsWalk", true);
    }
}
