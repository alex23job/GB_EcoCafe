using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanPoint : MonoBehaviour
{
    [SerializeField] private GameObject _trashCanteiner;

    private TrashCanControl _control;

    private void Awake()
    {
        _control = _trashCanteiner.GetComponent<TrashCanControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TrashCanControl CanteinerControl
    {
        get { return _control; }
    }
}
