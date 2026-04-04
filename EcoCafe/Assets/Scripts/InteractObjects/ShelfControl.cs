using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfControl : MonoBehaviour, IMyCommand
{
    [SerializeField] private GameObject _cabbageBox;

    private float _timer;
    private bool _isUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        HideBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isUsed)
        {
            if (_timer > 0) _timer -= Time.deltaTime;
            else
            {
                _isUsed = false;
                HideBox();
            }
        }
    }
    public void Execute(int numCommand)
    {
        if (_cabbageBox != null)
        {
            _cabbageBox.SetActive(true);
            _timer = 120f;
            _isUsed = true;
        }
    }

    public void HideBox()
    {
        if (_cabbageBox != null) _cabbageBox.SetActive(false);
    }
}
