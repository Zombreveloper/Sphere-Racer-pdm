using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAnimation : MonoBehaviour
{
	//private GameObject spaceFlame;
	float XRandom, YRandom, ZRandom;
	
    // Start is called before the first frame update
    void Start()
    {
        //spaceFlame = this.GameObject;
		
    }

    // Update is called once per frame
    void Update()
    {
		XRandom = Random.Range(0.1416146f, 0.2f);
		YRandom = Random.Range(0.7f, 0.8f);
		ZRandom = Random.Range(0.1f, 0.2f);
		
       transform.localScale = new Vector3 (XRandom, YRandom, ZRandom); 
    }
}
