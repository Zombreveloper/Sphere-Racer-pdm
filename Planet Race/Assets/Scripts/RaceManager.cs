using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public GameObject playerCar;
    private Rigidbody playerCarRB;
    public Transform startPoint; //leres Object mit korrektem Transform für startendes Auto

    public TinyPlanetGravitation myPlanet; //Gravitationsberechnung für vector und positions zeug

    private string planetImOn;

    public LoadCar _loadCar;

    public Transform checkPointMesh; //Mesh bzw Modell vom aktiven Checkpoint
    private Transform parentOfCheckPoints; //Parent von allen Checkpoints, heißt "AllCheckPoints"
    private int numberOfCheckPoints;
    private int numberOfActive; //Nummer in RaceManager
    public int nameOfActive; //Name der Objecte der CheckPointScricpts
    private int numberOfResetPos; //letzter von Auto durchfahrener Checkpoint
    private int checkPointLayer;
    public bool triggerStatus { get; set; }

    private float startingTime;
    private float endingTime;
    private float totalTime;

    public bool countsLaps { get; set;}
    private int lapCounter = 0;
    public int totalLaps = 2;

    public GameObject endeText;

//hauptprogramm
    void Awake()
    {
        setupCheckPoints();
        checkPointLayer = LayerMask.NameToLayer("CheckPoint");
        planetImOn = PlayerPrefs.GetString("selectedPlanet");
        //endeText = gameObject.Find(endText);
    }

    void Start()
    {
        startRace();
        triggerStatus = false;

        playerCar = GameObject.Find("currentCar");
        playerCarRB = playerCar.GetComponent<Rigidbody>();
        myPlanet = playerCar.GetComponent<TinyPlanetGravitation>(); //tinyplanet vom car getten
    }

    void Update()
    {
        //setzt Mesh auf naechste Checkpoints
        updateCheckPoints();

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetCar();
        }
    }

/*
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != checkPointLayer)
        {
            return;
        }
        else
        {
            triggerStatus = true;
            Debug.Log("enter Trigger RaceManager");
        }
    }
*/

//methoden
    void setupCheckPoints()
    {
        parentOfCheckPoints = GameObject.Find("AllCheckPoints").transform;
        numberOfCheckPoints = parentOfCheckPoints.childCount;
        numberOfActive = 0; //startwert, collider der Ziellinie
        numberOfResetPos = 0;
    }

    void startRace()
    {
        positionMesh(numberOfActive);

        //setzt Auto an StartOrt
        /*playerCar.position = startPoint.position;
        playerCar.rotation = startPoint.rotation;*/

        //Startet counter (3, 2, 1, GO!)

        //Nach GO! startzeit nehmen
        startingTime = Time.time;
        endeText.SetActive(false);

        //Startet anschliessend Musik
    }

    void updateCheckPoints()
    {
        if (triggerStatus)
        {
            //Debug.Log("nameOfActive = " + nameOfActive);
            //Debug.Log("numberOfActive = " + numberOfActive);
            if (nameOfActive == numberOfActive)
            {
                updateLaps();
                if (numberOfActive == numberOfCheckPoints-1)
                {
                    numberOfResetPos = numberOfActive;
                    numberOfActive = 0;
                    positionMesh(numberOfActive);
                }
                else
                {
                    numberOfResetPos = numberOfActive;
                    numberOfActive++;
                    positionMesh(numberOfActive);
                }
            }
        }
        //bei Trigger numberOfActive++; + positionMesh();
    }

    void positionMesh(int numberOfPoint)
    {
        //transform holen
        Transform posOfPoint = parentOfCheckPoints.transform.GetChild (numberOfPoint); //0 bis childCount

        checkPointMesh.position = posOfPoint.position;
        checkPointMesh.rotation = posOfPoint.rotation;
        triggerStatus = false;
    }

    public void resetCar()
    {
        playerCarRB.velocity = Vector3.zero; //geschwindigkeit auf 0 setzen
        Transform posOfPoint = parentOfCheckPoints.transform.GetChild (numberOfResetPos); //transform des letzten checkpoints holen

        /*playerCar.transform.position = posOfPoint.position;  + new Vector3(0,4,0); sollt immer \
        oben relativ zu planet, ist aber oben relativ zu koordinatensystem*/
        //nich += denn es soll insgesamt auch bei mehrmaligem drücken nur eimal (1) 3 aud die höhe addiert werden

        Vector3 upDirection = myPlanet.directionOfGravity;
        playerCar.transform.position = posOfPoint.position + Vector3.Normalize(upDirection)*-3;
        //auto landet an Position des Checkpoints, aber erhöht um 3 entlang der Achse zwischen Planetkern und Auto

        //weil die CheckPoints alle in unterschiedliche Richtungen gucken... unterschiedliche rotation am ziel
        if (planetImOn == "Mars")
        {
            playerCar.transform.rotation = posOfPoint.rotation * Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation
        }
        else if (planetImOn == "Mond")
        {
            playerCar.transform.rotation = posOfPoint.rotation * Quaternion.Euler(0, -90, 0);
        }
        else if (planetImOn == "Bambus")
        {
            playerCar.transform.rotation = posOfPoint.rotation * Quaternion.Euler(0, 0, 0);
        }
        else if (planetImOn == "BigBlueOcean")
        {
            playerCar.transform.rotation = posOfPoint.rotation * Quaternion.Euler(0, 0, 0);
        }

        //Debug.Log("Reset the Car NOW!!!");
    }

    void updateLaps()
    {
        if (countsLaps) //If the car collides wit a finish-line then...
        {
            lapCounter++; //..the car is now in the next Lap.
            Debug.Log("You are in Lap Number " + lapCounter);
            countsLaps = false;
        }

        if (lapCounter == totalLaps+1)
        {
            gameEnd();
            //Debug.Log("You WON!");
        }
    }

    void gameEnd()
    {
        endingTime = Time.time;
        totalTime = endingTime - startingTime;
        PlayerPrefs.SetFloat("totalTime", totalTime);

        //schriftzug
        endeText.SetActive(true);

        Invoke("goMenu", 5 );

        //ins Menu zurück
    }

    void goMenu()
    {
        SceneManager.LoadScene("Players Score");
    }
}
