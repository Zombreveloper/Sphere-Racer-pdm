using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 300f;
    public float maxSteer = 20f;
    public float accelerationOn = 10;
    public float brakePower = 1000;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    private float oldThrottle;

    public float myVelocity { get; private set; }

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
        myVelocity = _rigidbody.velocity.magnitude;

        oldThrottle = Throttle; //oldThrottle bekommt immer den vorherigen Throttle wert, umd veränderung zu identifizieren
        Steer = GameManager.Instance.InputController.SteerInput; //aktueller wert der Horizontal-Achse
        Throttle = GameManager.Instance.InputController.ThrottleInput; //aktueller Wert der Vertikal-Achse

        /*
        foreach (var wheel in wheels)
        {
            //Car gibt Werte 1, 0, -1 multipliziert mit Kraft an Wheel-Variablen weiter
            wheel.SteerAngle = Steer * maxSteer; //wheel ist quasie Listener von SteerAngle oder Torque
            wheel.Torque = Throttle * motorTorque;
        }
        */

        //Debug.Log("Geschwinfigkeit: " + myVelocity); //Testzweck

        foreach (var wheel in wheels)
        {
            //Car gibt Werte 1, 0, -1 multipliziert mit Kraft an Wheel-Variablen weiter
            wheel.SteerAngle = Steer * maxSteer; //wheel ist quasie Listener von SteerAngle oder Torque
            wheel.Brake = 0;

            if (Throttle == 1 && myVelocity > accelerationOn) //vorwärts beschläunigen
            {
                wheel.Torque = Throttle * motorTorque;
            }
            else if (Throttle == 1 && myVelocity < accelerationOn) //vorwärts fahren (normal)
            {
                wheel.Torque = Throttle * motorTorque * 3;
            }
            /*else if (oldThrottle == 1 && Throttle == 0) //wenn w / UP losgelassen wird
            {
                while (Throttle == 0)
                {
                    wheel.Brake = 50;
                }
                wheel.Brake = 50;
            }*/
            else if (Throttle == 0)
            {
                wheel.Torque = Throttle * motorTorque;
            }
            else if (myVelocity > 3 && Throttle == -1) //wenn von (w / UP) hohe geschw.  auf s / DOWN bremsen
            {
                    wheel.Brake = brakePower;
            }
            else if (myVelocity < 3 && Throttle == -1) //wenn bei quasie 0 geschw. auf s / DOWN bzw. rückwärts fahren
            {
                wheel.Torque = Throttle * motorTorque;
            }
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
				return speed / 100; //Geschwindigkeit durch mögliche Höchstgeschw. Bisher experimentelle Größe
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
