using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPowerControl : MonoBehaviour
{
    [SerializeField] private Transform _windMotor;
    [SerializeField] private Transform _propeller;
    [SerializeField] private float _delayDirection = 30f;
    //[SerializeField] private HingeJoint _propellerHJ;
    //[SerializeField] private HingeJoint _motorHJ;
    [SerializeField] private float _speed;

    private float _timer = 30f;
    private float _angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        _timer = _delayDirection;
        // Настраиваем мотор для непрерывного вращения
        //JointMotor motor = _propellerHJ.motor;
        //motor.targetVelocity = _speed;
        //motor.force = 100f; // Сила мотора
        //_propellerHJ.motor = motor;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 rotProp = _propeller.localRotation.eulerAngles;

        //rotProp.x += 20f * Time.deltaTime;
        _angle += _speed * Time.deltaTime;
        _propeller.localRotation = Quaternion.Euler(new Vector3(_angle, 0, 0));
        if (_timer > 0) _timer -= Time.deltaTime;
        else
        {
            _timer = _delayDirection;
            Vector3 rotMotor = _windMotor.localRotation.eulerAngles;
            //int deg = Random.Range(0, 90) + (int)rotMotor.y;
            //rotMotor.y = deg % 360;
            int deg = Random.Range(0, 90) + (int)rotMotor.z;
            rotMotor.z = deg % 360;
            _windMotor.localRotation = Quaternion.Euler(rotMotor);
        }
        //Vector3 rotProp = _propeller.localRotation.eulerAngles;
        //rotProp.x += 20f * Time.deltaTime;
        //_propeller.localRotation = Quaternion.Euler(rotProp);
        /*if (_timer > 0) _timer -= Time.deltaTime;
        else
        {
            _timer = _delayDirection;
            //Vector3 rotMotor = _windMotor.rotation.eulerAngles;

            int deg = Random.Range(0, 90);
            //rotMotor.y = deg % 360;
            //_windMotor.rotation = Quaternion.Euler(rotMotor);
            // Обновляем верхний предел лимита
            _motorHJ.limits = new JointLimits { min = 0, max = deg };
            // Меняем целевой угол вращения через мотор
            JointMotor motor = _motorHJ.motor;
            motor.targetVelocity = 90f;
            print($"deg={deg}   Velocity={motor.targetVelocity}  angle={_motorHJ.angle}");
            _motorHJ.motor = motor;
            Rigidbody motRB = _motorHJ.gameObject.GetComponent<Rigidbody>();
            motRB.MoveRotation(Quaternion.Euler(0, deg, 0));
        }*/
    }
}
