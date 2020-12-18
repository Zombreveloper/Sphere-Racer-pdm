using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //public um z. B. statt Tastatur nen Controller zu benutzen
    public string inputSteerAxis = "Horizontal";
    public string inputThrottleAxis = "Vertical";

    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }

    void Start()
    {

    }

    void Update()
    {
        SteerInput = Input.GetAxis(inputSteerAxis);
        ThrottleInput = Input.GetAxis(inputThrottleAxis);
    }
}
