using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField] private GameObject[] _foodPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetFood(int type)
    {
        GameObject food = null;
        if (type <= 1 )
        {
            food = Instantiate(_foodPrefabs[0]);
        }
        if (type == 2)
        {
            food = Instantiate(_foodPrefabs[1]);
        }
        if (type == 3)
        {
            food = Instantiate(_foodPrefabs[2]);
        }

        return food;
    }
}
