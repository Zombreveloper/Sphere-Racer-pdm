using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{	
	
	public FmodVolumeSettings mySoundmanager;
	
    public static bool GameIsPaused = false;
	

    public GameObject pauseMenuUI;
	public GameObject OptionsMenuUI;
	

	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
		OptionsMenuUI.SetActive(false);
		mySoundmanager.UnpauseSFX();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
		mySoundmanager.PauseSFX();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
        PlayerPrefs.SetString("fromPLanet", "Splash");
        PlayerPrefs.Save();
		mySoundmanager.UnpauseSFX();
        SceneManager.LoadScene("Menu");
    }
	
	public void goToOptions()
	{
		pauseMenuUI.SetActive(false);
		OptionsMenuUI.SetActive(true);
		
	}
	
	public void goToPauseMenu()
	{
		OptionsMenuUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}

    public void QuitGame()
    {
        Application.Quit(); //spiel verlassen
    }
}
