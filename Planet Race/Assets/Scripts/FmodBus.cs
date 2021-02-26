using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodBus : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;
    FMOD.Studio.Bus bus;
    
    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField] [Range(-80f, 10f)]
    private float busVolume;
    private float volume;

    void Start()
    {        
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();

        bus = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Vehicles");
    }
    void Update()
    {       
        volume = Mathf.Pow(10.0f, busVolume / 20f);
        bus.setVolume(volume);
    }
}