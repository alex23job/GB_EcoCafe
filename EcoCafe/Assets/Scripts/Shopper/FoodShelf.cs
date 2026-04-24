using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShelf : MonoBehaviour
{
    [SerializeField] private GameObject[] _foodPackets;

    private int _foodCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _foodCount = 5;
        ViewPackets();
    }

    public bool GetFood()
    {
        if (_foodCount > 0)
        {
            _foodCount--;
            ViewPackets();
            return true;
        }
        return false;
    }

    public void AddFood()
    {
        if (_foodCount < _foodPackets.Length)
        {
            _foodCount++;
        }
        Invoke("ViewPackets", 0.5f);
    }

    private void ViewPackets()
    {
        for (int i = 0; i < _foodPackets.Length; i++) 
        {
            if (i < _foodCount)
            {
                _foodPackets[i].gameObject.SetActive(true);
            }
            else
            {
                _foodPackets[i].gameObject.SetActive(false);
            }
        }
    }
}
