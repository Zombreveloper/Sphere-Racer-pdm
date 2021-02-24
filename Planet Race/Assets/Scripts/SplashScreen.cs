using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private string currentPlanet;

    // Start is called before the first frame update
    void Start()
    {
        currentPlanet = "Splash";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goMenu()
    {
        PlayerPrefs.SetString("fromPLanet", currentPlanet);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
    }
}
