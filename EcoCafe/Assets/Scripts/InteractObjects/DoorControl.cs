using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour, IMyCommand
{
    [SerializeField] private float _maxVelocity = 10f;
    private HingeJoint _hingeJoint;
    private JointMotor _motor;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _motor = _hingeJoint.motor;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Execute(int numCommand)
    {
        if (numCommand == 1)
        {
            _motor.targetVelocity = -_maxVelocity;
            _motor.force = _maxVelocity;
            _hingeJoint.motor = _motor;
            _hingeJoint.useMotor = true;
            Invoke("EndMotor", 5f);
        }
        if (numCommand == 2)
        {
            _motor.targetVelocity = _maxVelocity;
            _motor.force = _maxVelocity;
            _hingeJoint.motor = _motor;
            _hingeJoint.useMotor = true;
            Invoke("EndMotor", 5f);
        }
    }

    public void EndMotor()
    {
        _hingeJoint.useMotor = false;
    }
}
