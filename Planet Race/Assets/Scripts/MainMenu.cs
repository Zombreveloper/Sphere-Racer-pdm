using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //private GameObject planet;
    //public PlanetSelection PlanetSelection;
    public int currentPlanet;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (PlayerPrefs.HasKey("myResolution"))
            {
                int resolutionIndex = PlayerPrefs.GetInt("myResolution");
                SetResolution(currentResolutionIndex);
                currentResolutionIndex = resolutionIndex;
            }
            else if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("myResolution", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void updateCurrentPlanet(int planet)
    {
        currentPlanet = planet;
        Debug.Log(planet);
    }

    public void Play()
    {
        //IDEE
        //SceneManager.LoadScene("Mars"); //Testzweck, besser abfrage was der spiler gewählt hat...

        //VERSION 1
        //schlechter code: anzahl der Planeten variable, namen sollten dynamisch, nicht total sein
        /*
        if (currentPlanet == 0)
        {
            //SceneManager.LoadScene("Mond");
            Debug.Log("Lade Mond Szene");
        }
        else if (currentPlanet == 1)
        {
            SceneManager.LoadScene("Mars");
        }
        else if (currentPlanet == 2)
        {
            //SceneManager.LoadScene("Wasser");
            Debug.Log("Lade Wasser Szene");
        }
        else if (currentPlanet == 3)
        {
            //SceneManager.LoadScene("Bambus");
            Debug.Log("Lade Bambus Szene");
        }*/

        //VERSION 2
        //SceneManager.LoadScene("GameSceneTest");

        //VERSION 3
        Debug.Log("Menu Planet: " + PlayerPrefs.GetString("selectedPlanet"));
        SceneManager.LoadScene(PlayerPrefs.GetString("selectedPlanet"));
    }

    public void Quit()
    {
        Debug.Log("QUIT!!!!!!");
        Application.Quit(); //spiel verlassen
    }

    public void devLasseDeleteScoreLists()
    {
        //Löscht Lasses Listen zu Testzwecken, bitte nicht benutzen...
        PlayerPrefs.DeleteKey("highscoreTable");
        PlayerPrefs.DeleteKey("BigBlueOcean");
        PlayerPrefs.DeleteKey("Bambus");
        PlayerPrefs.DeleteKey("Mond");
        PlayerPrefs.DeleteKey("Mars");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
