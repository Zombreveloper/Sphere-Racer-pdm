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
		
		
		target = _manager.checkPointMesh; // Position des nächsten Checkpoints
		
		
		// Rotation auf diesen.
		Vector3 targetPosition = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(targetPosition);
		transform.rotation = rotation;
		
	}
	
	
	
	
	
	
}