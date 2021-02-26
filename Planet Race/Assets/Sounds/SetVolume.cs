using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SetVolume : MonoBehaviour {

    public AudioMixer mixer;
	public Slider slider;
	
	void Start()
	{
		if (slider) {
		slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
		Debug.Log("SetVolume.cs -> void Awake: Der Slider existiert und wurde gesetzt");
		}
		else {
			//Debug.Log("SetVolume.cs -> void Awake: Diese Szene hat keinen aktiven Slider. Werte direkt aus PlayerPrefs in den AudioMixer geben");
			float savedVolume = 0;
			
			savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
				Debug.Log("Die aktuelle Lautstärke ist " + savedVolume);
			savedVolume = Mathf.Log10(savedVolume) * 20;
				//ebug.Log("Der AudioMixer bekommt: " + savedVolume);
			
			mixer.SetFloat("MusicVol", savedVolume); 
		}
	}

    public void SetLevel (float sliderValue)
    {
		float mixerVolume = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("MusicVol", mixerVolume);
		PlayerPrefs.SetFloat("MusicVolume", sliderValue);
		Debug.Log("SetVolume.cs -> void SetLevel: Der SliderValue ist " + sliderValue);
    }
	
	public void OnDisable () {
		PlayerPrefs.Save();
		
	}
}



