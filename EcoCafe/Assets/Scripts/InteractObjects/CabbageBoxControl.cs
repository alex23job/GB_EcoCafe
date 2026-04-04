using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageBoxControl : MonoBehaviour, ITaking
{
    [SerializeField] private int _id = 0;
    [SerializeField] private int _price = 10;
    [SerializeField] private string _descr = "";
    private Vector3 _startPosition;
    private bool _isBoxHide = true;

    public int Price { get { return _price; } }
    public int TakingID { get { return _id; } }
    public int TakingPrice { get { return _price; } }

    public string Description { get => _descr; set { _descr = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject TakingItem()
    {
        return gameObject;
    }

    public void CreateBox()
    {
        if (_isBoxHide)
        {
            transform.position = _startPosition;
            _isBoxHide = false;
        }
    }

    public void HideItem()
    {
        HideBox();
    }

    public void HideBox()
    {
        Vector3 hidePos = _startPosition;
        hidePos.y = -1.5f;
        transform.position = hidePos;
    }
}
