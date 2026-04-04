using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    [SerializeField] private string _pointDescr;
    [SerializeField] private GameObject _linkTarget;
    [SerializeField] private int _numberFirstCommand;
    [SerializeField] private int _numberSecondCommand;
    [SerializeField] private int _numberThreeCommand;
    [SerializeField] private int _idTaikingItem = -1;

    private IMyCommand _myCommand = null;
    private bool _inTrigger = false;
    public string Description { get {  return _pointDescr; } }
    public int ID_TaikingItem { get {  return _idTaikingItem; } }

    private void Awake()
    {
        if (_linkTarget != null) _myCommand = _linkTarget.GetComponent<IMyCommand>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_myCommand != null) _myCommand.Execute(_numberFirstCommand);
            }
        }
    }

    public void ChangeUsed(bool value)
    {
        _inTrigger = value;
        if (value == false)
        {
            if (_myCommand != null) _myCommand.Execute(_numberSecondCommand);
        }
    }
}
