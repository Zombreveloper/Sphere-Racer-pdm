using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothAudiotransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
	//Hier wird eine Instanz der momentanen Szene erstellt, die nur das 
	//Objekt enthält, an dem das Script hängt
	private static SmoothAudiotransition instance = null;
	public static SmoothAudiotransition Instance 
	{
		get {return instance;}
	}

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}	
		
		// Die Musik soll mal nur nach dem Splashscreen weiterspielen
		string currentPlanet = PlayerPrefs.GetString("fromPLanet", "crash");
		Debug.Log("Die jetzige Scene ist: " + currentPlanet);
		
			
			DontDestroyOnLoad(this.gameObject);
	}	
	

    // Update is called once per frame
    void Update()
    {
        
    }
}
