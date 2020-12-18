using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public Transform RaceManager;
    private RaceManager _manager;
    private string myName;
    public bool i_am_finish_line;

    void Awake()
    {
        _manager = GameObject.FindObjectOfType<RaceManager>();
        myName = this.name;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on trigger enter CheckPointScrpt");

        _manager.triggerStatus = true;
        _manager.nameOfActive = int.Parse(myName);
        _manager.countsLaps = i_am_finish_line;
    }
}
