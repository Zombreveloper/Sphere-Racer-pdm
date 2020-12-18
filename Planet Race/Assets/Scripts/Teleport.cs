using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Transform teleportTarget; //Teleport Position wo man hinteleportiert
	private GameObject thePlayer; //Spieler, der teleportiert wird

    void OnTriggerEnter(Collider other)
    {
        thePlayer = GameObject.Find(other.transform.root.name);
		//Debug.Log(thePlayer);
		teleport();
    }

    void teleport()
	{
		thePlayer.transform.position = teleportTarget.transform.position;
	}
}
