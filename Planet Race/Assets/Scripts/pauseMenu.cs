using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{	
	
	public FmodVolumeSettings mySoundmanager;
	
    public static bool GameIsPaused = false;
	

    public GameObject pauseMenuUI;
	

	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
				mySoundmanager.UnpauseSFX();
            }
            else
            {
                Pause();
				mySoundmanager.PauseSFX();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
		mySoundmanager.UnpauseSFX();
        PlayerPrefs.SetString("fromPLanet", "Splash");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit(); //spiel verlassen
    }
}
