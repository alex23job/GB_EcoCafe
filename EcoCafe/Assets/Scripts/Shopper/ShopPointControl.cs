using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPointControl : MonoBehaviour
{
    [SerializeField] private int _price = 5;
    [SerializeField] private SpawnFood _spawnFood;
    [SerializeField] private int _typeFood = 1;
    [SerializeField] private int _maxCountProduct;

    private int _countProduct = 0;
    private bool _isSale = false;
    public int TypeFood { get { return _typeFood; } }
    public int Price { get { return _price; } }
    public bool IsSale { get { return _isSale; } }

    // Start is called before the first frame update
    void Start()
    {
        if (_typeFood == 0) AddingProduct(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetFood()
    {
        if (_countProduct > 0)
        {
            _countProduct--;
            return _spawnFood.GetFood(_typeFood);
        }
        return null;
    }

    public void AddingProduct(int count)
    {
        if (count == -1)
        {
            _countProduct = _maxCountProduct;
            _isSale = true;
        }
        else
        {
            _countProduct += count;
            if (_countProduct > _maxCountProduct) _countProduct = _maxCountProduct;
        }
    }
}
