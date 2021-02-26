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
    public float accelerationMult = 3;//multiplicateor of motorTorque when accelerating

    public float Steer { get; set; }
    public float Throttle { get; set; }
    private float oldThrottle;

    public float myVelocity { get; private set; }

	private Rigidbody _rigidbody;
    private Wheel[] wheels; //Array aus reifen

    private int doIRev = 0;

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

        whatDoWeehlsDo();
    }

    private void whatDoWeehlsDo()//how wheels have to react to keyboard input
    {
        /*
        ICH WEI? NICHT GENAU WARUM ES FUNKTIONIERT; ABER; ES FUNKTIONIERT!!!!!!
        */
                
        foreach (var wheel in wheels)
        {
            //Car gibt Werte 1, 0, -1 multipliziert mit Kraft an Wheel-Variablen weiter
            wheel.SteerAngle = Steer * maxSteer; //wheel ist quasie Listener von SteerAngle oder Torque

            if (myVelocity <= 0.1 && Throttle == -1)//check if it should be driving in reverse
            {
                //Debug.Log("Car: doIRev is now true");
                doIRev = 1;
            }

            //Debug.Log("Car: myVelocity: " + myVelocity + "Car: Throttle: " + Throttle);
            if(Throttle == 1 && myVelocity < 0.1 && doIRev == 1)
            {
                doIRev = 0;
                Debug.Log("Car: doIRev ist wieder 0");
            }
            else if (Throttle == 1 && doIRev == 1)//reverse Bremse deaktivieren
            {
                wheel.Brake = brakePower;
            }

            //VORWÄRTS FAHREN
            else if (Throttle == 1 && myVelocity > accelerationOn) //vorwärts beschläunigen
            {
                wheel.Brake = 0;
                wheel.Torque = Throttle * motorTorque;
                doIRev = 0;
            }
            else if (Throttle == 1 && myVelocity < accelerationOn) //vorwärts fahren (normal)
            {
                wheel.Brake = 0;
                wheel.Torque = Throttle * motorTorque * accelerationMult;
                doIRev = 0;
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

            //VON BREMSE AUF RÜCKWÄRTS
            else if (Throttle == -1 && doIRev == 1) //wenn bei quasie 0 geschw. auf s / DOWN bzw. rückwärts fahren
            {
                //wheel.Brake = 0;
                //wheel.Torque = Throttle * motorTorque;
                Invoke("driveReverse", 1);
            }
            else if (myVelocity > 0 && Throttle == -1) //wenn von (w / UP) hohe geschw.  auf s / DOWN bremsen
            {
                    wheel.Brake = brakePower;
            }
        }
    }

    void driveReverse()
    {
        foreach (var wheel in wheels)
        {
            wheel.Brake = 0;
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
