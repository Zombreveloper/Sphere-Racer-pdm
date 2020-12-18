using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyPlanetGravitation : MonoBehaviour
{
    //public GameObject playerCar;
    //private Rigidbody rbToAttract; //Entsprechendes Auto -> Variable die durch Menu vorgegeben werden muss
    private Rigidbody rbToAttract;

    private Transform tinyPlanet;
    public float MassOfTinyPlanet = 100000f;

    public Vector3 directionOfGravity { get; private set; }
    public float gravity = 30f;
    private float forceOfGravity;
    private float distance;

    void Start()
    {
        /*
        GameObject playerCar = GameObject.Find(PlayerPrefs.GetString("selectetCar"));
        rbToAttract = playerCar.GetComponent<Rigidbody>();
        */
        //playerCar = GameObject.Find("carClone");
        rbToAttract = GetComponent<Rigidbody>();
        GameObject planetObj = GameObject.Find(PlayerPrefs.GetString("selectedPlanet"));
        tinyPlanet = planetObj.transform;
    }

    void FixedUpdate()
    {
        //forceOfGravity muss in richtung directionOfGravity auf objToAttract uebertragen werden.

        //folgender code funktioniert, ist aber bullshit
        //rbToAttract.AddForce(directionOfGravity.normalized * gravity);

        directionOfGravity = tinyPlanet.position - rbToAttract.position;
        distance = directionOfGravity.magnitude;

        //F=G*(m1*m2)/r^2 ->G=9.81, m1(Masse der Erde)=
        forceOfGravity = gravity * ((MassOfTinyPlanet * rbToAttract.mass)/Mathf.Pow(distance, 2));
        rbToAttract.AddForce(directionOfGravity.normalized * forceOfGravity);
    }
}
