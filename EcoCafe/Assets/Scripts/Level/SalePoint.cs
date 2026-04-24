using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalePoint : MonoBehaviour
{
    [SerializeField] private Text _saleText;
    [SerializeField] private string _descr;
    [SerializeField] private int _price;
    [SerializeField] private int _saleID;
    [SerializeField] private GameObject _linkObject;
    [SerializeField] private GameObject _destroyObject;
    [SerializeField] private LevelControl _levelControl;
    [SerializeField] private ShopPointControl _shopPoint;

    public string Description { get { return _descr; } }
    public int SaleID { get { return _saleID; } }

    private bool _isUsed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isUsed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_levelControl != null)
                {
                    if (_levelControl.CheckManyAndSale(_price, _saleID))
                    {
                        _linkObject.SetActive(true);
                        if (_destroyObject != null) Destroy(_destroyObject);
                        Destroy(gameObject);
                        if (_shopPoint != null)
                        {
                            //_shopPoint.
                        }
                    }
                }
                //if (_myCommand != null) _myCommand.Execute(_numberFirstCommand);
            }
        }
    }

    public void SetSaleText(string txt)
    {
        _saleText.text = txt;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isUsed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isUsed = false;
        }
    }
}
