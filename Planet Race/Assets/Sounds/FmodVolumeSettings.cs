using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FmodVolumeSettings : MonoBehaviour {

     FMOD.Studio.EventInstance SFXVolumeTestEvent;
     FMOD.Studio.Bus SFX;
	 
	 public Slider slider;

     float SFXVolume;
	 int firstChanged = 0; //Flag, der aussagt, ob die Lautstärke schon mal geändert wurde

     void Start ()
	 {
		 if(slider)
		 slider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
     }
	 
	 void Awake ()
     {

          SFX = FMODUnity.RuntimeManager.GetBus ("bus:/SFX/Vehicles");
		  //SFXInstance = FMODUnity.RuntimeManager.CreateInstance ("bus:/SFX/Vehicles");
          SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance ("event:/Vehicles/OptionsReferenceSound");
		  SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
	 }	  

     void Update () 
     {
         SFX.setVolume (SFXVolume);
     }
	 
	 public void PauseSFX()
	 {
		 SFX.setPaused (true);
	 }
	 
	 public void UnpauseSFX()
	 {
		 SFX.setPaused (false);
	 }


     public void SFXVolumeLevel (float newSFXVolume)
     {
		 
		 if (firstChanged == 0)
		 {
		 
          SFXVolume = newSFXVolume;
		  PlayerPrefs.SetFloat("SFXVolume", newSFXVolume);
		  firstChanged ++;
		 }
		else 
		{
		  SFXVolume = newSFXVolume;
		  PlayerPrefs.SetFloat("SFXVolume", newSFXVolume);
		  
          //Spielt Testsound ab, wenn die Lautstärke geändert wird
		  FMOD.Studio.PLAYBACK_STATE PbState;
          SFXVolumeTestEvent.getPlaybackState (out PbState);
          if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
          {
               SFXVolumeTestEvent.start ();
          }
		}
     }
}
