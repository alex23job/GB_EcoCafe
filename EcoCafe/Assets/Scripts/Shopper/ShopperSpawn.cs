using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _shoppersPrefab;
    [SerializeField] private int _spawnInterval = 30;
    [SerializeField] private LevelControl _levelControl;
    [SerializeField] private Transform[] _pathPoints;

    private List<Vector3> _path1 = new List<Vector3>();
    private List<Vector3> _path2 = new List<Vector3>();
    private List<Vector3> _path3 = new List<Vector3>();
    private List<Vector3> _path4 = new List<Vector3>();

    private float _timer = 1f;
    private int _countSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        _path1.Add(_pathPoints[0].position);
        _path1.Add(_pathPoints[1].position);
        _path1.Add(_pathPoints[2].position);
        _path1.Add(_pathPoints[3].position);
        _path1.Add(_pathPoints[4].position);
        _path1.Add(_pathPoints[5].position);
        _path1.Add(_pathPoints[6].position);
        _path1.Add(_pathPoints[2].position);
        _path1.Add(_pathPoints[1].position);
        _path1.Add(transform.position);
        _path2.Add(_pathPoints[0].position);
        _path2.Add(_pathPoints[1].position);
        _path2.Add(_pathPoints[2].position);
        _path2.Add(_pathPoints[3].position);
        _path2.Add(_pathPoints[4].position);
        _path2.Add(_pathPoints[5].position);
        _path2.Add(_pathPoints[6].position);
        _path2.Add(_pathPoints[2].position);
        _path2.Add(_pathPoints[1].position);
        _path2.Add(_pathPoints[7].position);
        _path2.Add(_pathPoints[8].position);
        _path2.Add(_pathPoints[7].position);
        _path2.Add(transform.position);

        SpawnShopper();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
        else
        {
            _timer = 1f;
            _countSecond++;
            if (_countSecond >= _spawnInterval)
            {
                _countSecond = 0;
                SpawnShopper();
            }
        }
    }

    private void SpawnShopper()
    {
        int num = Random.Range(0, _shoppersPrefab.Length);
        GameObject shopper = Instantiate(_shoppersPrefab[num], transform.position, Quaternion.identity);
        ShopperMovement sm = shopper.GetComponent<ShopperMovement>();
        List<Vector3> currentPath = _path1;
        if (sm != null)
        {
            sm.SetPath(currentPath, _levelControl);
        }
    }
}
