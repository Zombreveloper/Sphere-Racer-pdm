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
    protected string nameInput;
    public GameObject inputField;

    void Awake()
    {
        //timeText.Text = GameObject.Find("Your Time");
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTime = PlayerPrefs.GetFloat("totalTime");
        timeText.text = $"Time: {(int)totalTime / 60}:{(totalTime) % 60:00.000} ";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void submit()
    {
        nameInput = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log("Der Name lautet: " + nameInput);
        PlayerPrefs.SetString("currentName", nameInput);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /*public void scoreMenu()
    {
        SceneManager.LoadScene("Menu");
    }*/
}
