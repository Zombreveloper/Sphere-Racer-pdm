using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreMenu : MonoBehaviour
{
    public TMP_Text timeText;
    private float totalTime;
    private string totalTimeFormated;
    protected string nameInput;
    public GameObject inputField;

    private ScoreList currentList;

    //public MenuMode _menuMode;

    void Awake()
    {
        //timeText.Text = GameObject.Find("Your Time");
        currentList = new ScoreList();
        //SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        //_menuMode = GameObject.Find("MenuMode");
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTime = PlayerPrefs.GetFloat("totalTime");
        totalTimeFormated = $"Time: {(int)totalTime / 60}:{(totalTime) % 60:00.000} ";
        timeText.text = totalTimeFormated;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void submit()
    {
        //Namen Eintragen
        nameInput = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log("Der Name lautet: " + nameInput);

        //wenn kein Name eingetragne, dann verhindern!

        PlayerPrefs.SetString("currentName", nameInput);

        //mainMenu();
        string currentPlanet = PlayerPrefs.GetString("selectedPlanet");
        //_menuMode.fromRace(currentPlanet);
        PlayerPrefs.SetString("fromPLanet", currentPlanet);
        PlayerPrefs.Save();
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        SceneManager.LoadScene("Menu");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
