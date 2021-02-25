using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SetVolume : MonoBehaviour {

    public AudioMixer mixer;
	public Slider slider;
	
	void Awake()
	{
		slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
		Debug.Log("SetVolume.cs -> void Awake: Die Funktion wird ausgeführt");
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



