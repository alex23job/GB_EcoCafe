using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GarbageBag : MonoBehaviour
{
    [SerializeField] private int _price = 1;

    public int Price { get { return _price; } }

    private float _moveSpeed = 30f;
    private Vector3 _target;
    private bool _isMoving = false;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            Vector3 dir = _target - transform.position; dir.y = 0f;
            _rb.MovePosition(transform.position + dir.normalized * _moveSpeed * Time.deltaTime);
            if (Mathf.Abs(dir.magnitude) < 0.2f)
            {
                _isMoving = false;
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(Vector3 tg)
    {
        _target = tg;
        _isMoving = true;
        _rb.isKinematic = true;
        BoxCollider bc = gameObject.GetComponent<BoxCollider>();
        if (bc != null) bc.enabled = false;
    }
}
