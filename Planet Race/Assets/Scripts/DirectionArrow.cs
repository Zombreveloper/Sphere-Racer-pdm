using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour {

	[SerializeField]
	private Transform target;
	public Transform RaceManager;
    private RaceManager _manager;

	public TinyPlanetGravitation myPlanet; //Gravitationsberechnung für vector und positions zeug
	public GameObject playerCar;//car that you want to follow


    void Awake()
    {
        _manager = GameObject.FindObjectOfType<RaceManager>();
    }

	void Start()
    {
        playerCar = GameObject.Find("currentCar");
		myPlanet = playerCar.GetComponent<TinyPlanetGravitation>(); //tinyplanet vom car getten
    }

	private void Update()
	{
		Vector3 upDirection = Vector3.Normalize(myPlanet.directionUp);

		target = _manager.checkPointMesh; // Position des nächsten Checkpoints

		// Rotation auf diesen.
		Vector3 targetPosition = (target.position + (upDirection*3)) - transform.position;//guckt ueber checkpoint Position
		Quaternion rotation = Quaternion.LookRotation(targetPosition);
		transform.rotation = rotation;
	}
}
