using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfControl : MonoBehaviour, IMyCommand
{
    [SerializeField] private GameObject _cabbageBox;
    [SerializeField] private GameObject _foodBox;
    [SerializeField] private GameObject _food;

    private float _timer = 10f;
    private bool _isUsedCabbage = false;
    private bool _isUsedFood = false;
    private int _foodTimer = 0;
    private int _cabbageTimer = 0;
    private int _createFoodTimer = 3;

    private Vector3 _startPosCabbage;
    private Vector3 _startPosFoodBox;

    public bool IsUsed {  get { return _isUsedCabbage || _isUsedFood; } }

    // Start is called before the first frame update
    void Start()
    {
        _startPosCabbage = _cabbageBox.transform.position;
        _startPosFoodBox = _foodBox.transform.position;
        HideBox(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
        else
        {
            _timer = 10f;
            if (_foodTimer > 0) _foodTimer--;
            if (_foodTimer == 0)
            {
                _isUsedFood = false;
                HideBox(2);
            }
            if (_cabbageTimer > 0) _cabbageTimer--;
            if (_cabbageTimer == 0)
            {
                _isUsedCabbage = false;
                HideBox(1);
            }
            if (_isUsedCabbage || _isUsedFood)
            {
                _createFoodTimer--;
                if (_createFoodTimer == 0)
                {
                    _createFoodTimer = 3;
                    ViewFoodPacket();
                }
            }
        }
    }
    public void Execute(int numCommand)
    {
        if (numCommand == 1)
        {
            if (_cabbageBox != null)
            {
                _cabbageBox.SetActive(true);
                _cabbageTimer = 12;
                _isUsedCabbage = true;
            }
        }
        if (numCommand == 2)
        {
            if (_foodBox != null)
            {
                _foodBox.SetActive(true);
                _foodTimer = 12;
                _isUsedFood = true;
            }
        }
    }

    public void HideBox(int num)
    {
        if (num == 0) 
        {
            if (_cabbageBox != null) _cabbageBox.SetActive(false);
            if (_foodBox != null) _foodBox.SetActive(false);
        }
        if (num == 1)
        {
            if (_cabbageBox != null)
            {
                _cabbageBox.transform.position = _startPosCabbage;
                _cabbageBox.SetActive(false);
            }
        }
        if (num == 2)
        {
            if (_foodBox != null)
            {
                _foodBox.transform.position = _startPosFoodBox;
                _foodBox.SetActive(false);
            }
        }
    }

    private void ViewFoodPacket()
    {
        _food.SetActive(true);
    }
}
