using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanControl : MonoBehaviour
{
    /// <summary>
    /// общий - 1, с сортировкой - 3
    /// </summary>
    [SerializeField] private int _mode = 1;
    [SerializeField] private GameObject[] _garbageBagsPrefab;
    [SerializeField] private Transform[] _bagsPoint;

    private List<GameObject> _bags = new List<GameObject>(); 

    private int _countTrash = 0;
    private int _countFoodTrash = 0;
    private int _countPlTrash = 0;
    private int _countCartonTrash = 0;
    private int _countBags = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            AddingTrash(1 + i % 4);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddingTrash(int id)
    {
        if (_mode == 3)
        {
            if (id == 1 || id == 2) _countFoodTrash++;
            if (id == 3) _countPlTrash++;
            if (id == 4) _countCartonTrash++;
        }
        if (_mode == 1) _countTrash++;
        GenerateBag();
    }

    private void GenerateBag()
    {
        Vector3 pos = _bagsPoint[_countBags].localPosition;
        bool isGenerate = false;
        if (_countTrash >= 5)
        {
            _countTrash = 0;
            GameObject bag = Instantiate(_garbageBagsPrefab[0]);
            bag.transform.parent = transform;
            bag.transform.localPosition = pos;
            //GameObject bag = Instantiate(_garbageBagsPrefab[0], pos, Quaternion.identity);
            _bags.Add(bag);
            isGenerate = true;
        }
        if (_countCartonTrash >= 5)
        {
            _countCartonTrash = 0;
            GameObject bag = Instantiate(_garbageBagsPrefab[3]);
            bag.transform.parent = transform;
            bag.transform.localPosition = pos;
            //GameObject bag = Instantiate(_garbageBagsPrefab[3], pos, Quaternion.identity);
            _bags.Add(bag);
            isGenerate = true;
        }
        if (_countFoodTrash >= 5)
        {
            _countFoodTrash = 0;
            GameObject bag = Instantiate(_garbageBagsPrefab[1]);
            bag.transform.parent = transform;
            bag.transform.localPosition = pos;
            //GameObject bag = Instantiate(_garbageBagsPrefab[1], pos, Quaternion.identity);
            _bags.Add(bag);
            isGenerate = true;
        }
        if (_countPlTrash >= 5)
        {
            _countPlTrash = 0;
            GameObject bag = Instantiate(_garbageBagsPrefab[2]);
            bag.transform.parent = transform;
            bag.transform.localPosition = pos;
            //GameObject bag = Instantiate(_garbageBagsPrefab[2], pos, Quaternion.identity);
            _bags.Add(bag);
            isGenerate = true;
        }
        if (isGenerate) _countBags++;
    }

    public GameObject GetTrashBag()
    {
        if (_countBags > 0 && _bags.Count > 0)
        {
            GameObject bag = _bags[0];
            bag.transform.parent = null;
            _countBags--;
            _bags.RemoveAt(0);
            return bag;
        }
        return null;
    }
}
