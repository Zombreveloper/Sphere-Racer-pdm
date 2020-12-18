using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelection : MonoBehaviour
{
    public GameObject[] planets; //Macht GameObject Planets zu einem Array
    public int selectedPlanet = 0; //index das Array
    //public int selectedPlanet { get; set; }
    public MainMenu mainMenu;

    public void NextPlanet()
    {
        planets[selectedPlanet].SetActive(false);
        selectedPlanet = (selectedPlanet + 1) % planets.Length;
        planets[selectedPlanet].SetActive(true);
    }

    public void PreviousPlanet()
    {
        planets[selectedPlanet].SetActive(false);
        selectedPlanet--;
        if (selectedPlanet < 0)
        {
            selectedPlanet += planets.Length;
        }
        planets[selectedPlanet].SetActive(true);
    }

    public void StartGame() //von MainMenü rufen lassen
    {
        Debug.Log("Aktiver Planet: " + planets[selectedPlanet].name);
        //PlayerPrefs.SetInt("selectedPlanet", selectedPlanet); //speichert Daten, zwischen szenen
        PlayerPrefs.SetString("selectedPlanet", planets[selectedPlanet].name); //speichert Daten, zwischen szenen
        //SceneManager.LoadScene("Mars", LoadSceneMode.Single);
    }
/*
    void Awake()
    {
        mainMenu = GameObject.FindObjectOfType<MainMenu>();
    }

    void Update()
    {
        mainMenu.updateCurrentPlanet(selectedPlanet);
    }*/
}
