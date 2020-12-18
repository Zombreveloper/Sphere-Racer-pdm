using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCar : MonoBehaviour
{
    public GameObject[] carPrefabs;
    private Transform spawnPoint;
    public GameObject carClone;
    TinyPlanetGravitation _Gravitation;

    void Awake()
    {
        spawnPoint = GameObject.Find("StartOrt").transform;

        int selectedCar = PlayerPrefs.GetInt("selectedCarNumber");
        GameObject prefab = carPrefabs[selectedCar];
        //carClone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        carClone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        carClone.name = "currentCar"; //um das Auto später wiederzufinden
    }
}
