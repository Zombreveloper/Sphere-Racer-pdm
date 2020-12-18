using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 300f;
    public float maxSteer = 20f;

    public float Steer { get; set; }
    public float Throttle { get; set; }

	private Rigidbody _rigidbody;
    private Wheel[] wheels; //Array aus reifen
	
	void awake()
	{
		
	}

    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>(); //referenz auf "Wheel" Skript
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void Update()
    {
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer; //wheel ist quasie Listener von SteerAngle oder Torque
            wheel.Torque = Throttle * motorTorque;
        }
    }
	
	public float LocalSpeed()
	{
		var veryLocalRigid = new Rigidbody();
		veryLocalRigid = _rigidbody;
		//if (canMove)
		{
			float dot = Vector3.Dot(transform.forward, veryLocalRigid.velocity);
			if (Mathf.Abs(dot) > 0.1f)
			{
				float speed = _rigidbody.velocity.magnitude;
				return speed / 100; //der Returnwert muss noch durch die Höchstgeschw. geteilt werden oder korellierende Größe
			}
			//return 0f;
		}
		/*else
		{
			// use this value to play kart sound when it is waiting the race start countdown.
			return Input.y;
		} */
		return 0f;
	}
}
