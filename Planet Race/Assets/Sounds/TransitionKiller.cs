using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SmoothAudiotransition.Instance.gameObject.GetComponent<AudioSource>().Destroy();
		if(SmoothAudiotransition.Instance.gameObject.GetComponent<AudioSource>())
		{
			Destroy(SmoothAudiotransition.Instance.gameObject.GetComponent<AudioSource>());
		}
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
