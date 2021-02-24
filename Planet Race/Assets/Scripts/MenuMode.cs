using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMode : MonoBehaviour
{
    private string fromPlanet;

    public GameObject mainMenu;
    public GameObject BBOScore;
    public GameObject BambusScore;
    public GameObject MarsScore;
    public GameObject MondScore;

    void Awake()
    {
        fromPlanet = PlayerPrefs.GetString("fromPLanet");
        PlayerPrefs.SetString("fromPLanet", "nothing");
    }

    void Start()
    {
        //SceneManager.activeSceneChanged += ChangedActiveScene;
        fromRace(fromPlanet);
    }

    void Update()
    {}
/*
    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";

            fromRace(fromPlanet);
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
    }
*/
    public void fromRace(string _planet)//bringt spieler direkt ins richtige ScoreMenu
    {
        mainMenu.SetActive(false);

        if (_planet == "BigBlueOcean")
        {
            BBOScore.SetActive(true);
        }
        else if (_planet == "Bambus")
        {
            BambusScore.SetActive(true);
        }
        else if (_planet == "Mond")
        {
            MondScore.SetActive(true);
        }
        else if (_planet == "Mars")
        {
            MarsScore.SetActive(true);
        }
        else if (_planet == "Splash")
        {
            mainMenu.SetActive(true);
        }
        else if (_planet == "nothing")
        {
            return;
        }
        else
        {
            Debug.Log("ERROR: MenuModes Planet ist falsch!");
        }
    }
}
