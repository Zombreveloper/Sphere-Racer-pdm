using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;//offset the camera has relative to its target
    //[SerializeField] public Transform target;//car that you want to follow
    [SerializeField] public float translateSpeed;
    [SerializeField] public float rotationSpeed;

    public GameObject playerCar;//car that you want to follow
    private Transform playerCarT;
    public TinyPlanetGravitation myPlanet; //Gravitationsberechnung für vector und positions zeug

    void Start()
    {
        playerCar = GameObject.Find("currentCar");
        FindTarget();
        setOffset();
    }

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void FindTarget()
    {
        playerCarT = playerCar.GetComponent<Transform>();
        myPlanet = playerCar.GetComponent<TinyPlanetGravitation>(); //tinyplanet vom car getten
    }

    private void setOffset()
    {
        string myCar = PlayerPrefs.GetString("selectedCar");
        Debug.Log("Camera Follow: myCar: " + myCar);

        if (myCar == "Toyota GT 86")
        {
            offset = new Vector3 (0f, 3f, -5f);
        }
        else if (myCar == "Raumschiff")
        {
            offset = new Vector3 (0f, 8f, -18f);
        }
        else if (myCar == "PandaCar")
        {
            offset = new Vector3 (0f, 3.8f, -7.5f);
        }
        else if (myCar == "HippieVan")
        {
            offset = new Vector3 (0f, 4f, -6.3f);
        }
        else
        {
            Debug.Log("ERROR: CameraFollow no car found!");
        }
    }

    private void HandleTranslation()
    {
        //var targetPosition = target.TransformPoint(offset);//cameraPosition relative to targetPosition
        var targetPosition = playerCarT.TransformPoint(offset);//cameraPosition relative to targetPosition

        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 upDirection = Vector3.Normalize(myPlanet.directionUp);

        //var direction = target.position - transform.position;//diraction from cameraPosition to targetPositionos
        var direction = (playerCarT.position + (upDirection*2)) - transform.position;//diraction from cameraPosition to targetPositionos

        //var rotation = Quaternion.LookRotation(direction, Vector3.up);//Up vector ersetzten!!!
        var rotation = Quaternion.LookRotation(direction, upDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
