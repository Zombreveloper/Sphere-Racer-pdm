using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour {
	
	[SerializeField]
	private Transform target;
	public Transform RaceManager;
    private RaceManager _manager;

	
    void Awake()
    {
        _manager = GameObject.FindObjectOfType<RaceManager>();
     
    }
	
	
	private void Update() 
	{
		//Pfeilrichtung = Vektorrichtung, Vektor von Pfeil zu aktuellem Checkpoint
		// Vector3.RotateTowards(Vector3 current, Vector3 target)
		
		
		target = _manager.checkPointMesh;

		//transform.LookAt(target.position);
		
		Vector3 targetPosition = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(targetPosition);
		transform.rotation = rotation;
		
	
		
		//targetPosition.y = transform.position.y;
		
		//transform.LookAt(targetPosition);
		
		//playerCar.transform.position = posOfPoint.position + Vector3.Normalize(upDirection)*-4;
	}
	
	
	
	
	
	
}