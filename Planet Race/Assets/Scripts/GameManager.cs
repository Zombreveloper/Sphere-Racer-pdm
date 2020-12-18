using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Designpattern: Singleton (Eigentlich nicht nutzen...)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }

    public GameObject[] carPrefabs;
    public Transform spawnPoint;

    void Awake()
    {
        Instance = this;
        //Singleton, andere können "Instance" jetzt über diese stativ-Variable finden
        // nur ein (1) GameManager möglich zu verbinde -> Listener-Pattern?!
        InputController = GetComponentInChildren<InputController>();
    }
/*
    //Prüfen ob das so richtig ist (Suche: unity instaciate)
    void LoadCar()
    {
        string selectedCar = PlayerPrefs.GetString("selectedCar");
        GameObject prefab = carPrefabs[selectedCar];
        GameObject clonedPrefab = Instantiate(prefab);
    }
*/
}
