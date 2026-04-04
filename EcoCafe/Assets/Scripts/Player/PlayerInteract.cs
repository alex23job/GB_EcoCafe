using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LevelControl _levelControl;
    [SerializeField] private Transform _boxPoint;
    [SerializeField] private Transform _trashPoint;
    private GameObject _currentTarget;
    private ITaking _currentTaking;
    private int _currentTriggerItemID = -1;
    public Vector3 BoxPoint {  get { return _boxPoint.position; } }
    public Vector3 TrashPoint { get {return _trashPoint.position; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CabbageBox"))
        {
            _currentTaking = other.gameObject.GetComponent<ITaking>();
            if (_currentTaking != null)
            {
                _currentTarget = _currentTaking.TakingItem();
                _levelControl.ViewHint($"Нажмите \'E\' чтобы взять {_currentTaking.Description}");
            }
        }
        if (other.CompareTag("Trash"))
        {

        }
        if (other.CompareTag("Trigger"))
        {
            TriggerControl tc = other.GetComponent<TriggerControl>();
            if (tc != null)
            {
                _levelControl.ViewHint($"Нажмите \'E\' чтобы {tc.Description}");
                tc.ChangeUsed(true);
                _currentTriggerItemID = tc.ID_TaikingItem;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CabbageBox"))
        {
            _levelControl.HideHint();
            _currentTaking = null;
            _currentTarget = null;
        }
        if (other.CompareTag("Trash"))
        {
            _levelControl.HideHint();
            _currentTaking = null;
            _currentTarget = null;
        }
        if (other.CompareTag("Trigger"))
        {
            TriggerControl tc = other.GetComponent<TriggerControl>();
            if (tc != null)
            {
                _levelControl.HideHint();
                tc.ChangeUsed(false);
            }
        }
    }

    public int GetCurrentTakingID
    {
        get
        {
            if (_currentTaking == null) return -1;
            else return _currentTaking.TakingID;
        }
    }

    public void CarriasItem()
    {
        if (_currentTaking != null && _currentTarget != null)
        {
            if (_currentTaking.TakingID < 10)
            {   //  нести двумя руками
                _currentTarget.transform.position = _boxPoint.position;
                _currentTarget.transform.parent = _boxPoint;
                _currentTarget.transform.localRotation = Quaternion.identity;
            }
            else
            {   //  нести в одной руке
                _currentTarget.transform.position = _trashPoint.position;
                _currentTarget.transform.parent = _trashPoint;
                _currentTarget.transform.localRotation = Quaternion.identity;
            }
        }
    }

    public void ClearItem()
    {
        if (_currentTaking != null && _currentTarget != null)
        {
            if (_currentTaking.TakingID == _currentTriggerItemID)
            {
                _levelControl.ChangeMany(_currentTaking.TakingPrice);
                _currentTaking.HideItem();
                _currentTaking = null;
                _currentTarget = null;
            }
        }
    }
}
