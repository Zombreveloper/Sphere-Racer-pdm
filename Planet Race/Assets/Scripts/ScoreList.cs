using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreList : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    public string myPlanetName;//muss in der unity Oberfläche eingetragen werden

    //for ADDing A DEFAULT List (see line 23/24)
    private List<HighscoreEntry> highscoreEntryList;

    private Highscores highscores;


    void Awake()
    {
        entryContainer = transform.Find("newScoreContainer" + myPlanetName);
        entryTemplate = entryContainer.Find("newScoreTemplate"  + myPlanetName);

        entryTemplate.gameObject.SetActive(false);//muster-Zeile unsichtbar machen

        int addHigh = PlayerPrefs.GetInt("AddHigh");
        if (addHigh == 0)
        {
            //show the List (put things in order)
            showList();
        }
        else if (addHigh == 1)
        {
            makeEntry();
            PlayerPrefs.SetInt("AddHigh", 0);
            PlayerPrefs.Save();
            showList();
        }
        else
        {
            Debug.Log("ScoreList: ERROR: addHigh has wrong number!");
        }
    }


    private void makeEntry()
    {
        //AddHighscoreEntry(10000, "neuer n00b" ,"toyota"); //Testzweck

        //get the components
        string currentPlanet = PlayerPrefs.GetString("selectedPlanet");
        float totalTime = PlayerPrefs.GetFloat("totalTime");
        string nameInput = PlayerPrefs.GetString("currentName");

        string playerCar = PlayerPrefs.GetString("selectedCar");
        //Namen anpassen, weil wir doof sind :´)
        if (playerCar == "Toyota GT 86")
        {
            playerCar = "Yotota TG 68";
        }
        else if (playerCar == "Raumschiff")
        {
            playerCar = "Space Ship";
        }
        else if (playerCar == "PandaCar")
        {
            playerCar = "Panda Car";
        }
        else if (playerCar == "HippieVan")
        {
            playerCar = "Hippie Van";
        }

        Debug.Log("ScoreList: currentPlanet: " + currentPlanet);
        Debug.Log("ScoreList: myPlanetName: " + myPlanetName);

        //check if its the right planet
        if (currentPlanet == myPlanetName)
        {
            //now realy add it to list...
            Debug.Log("ScoreList: Planet ist richtig: " + myPlanetName);
            AddHighscoreEntry(currentPlanet, totalTime, nameInput , playerCar);
        }
    }

    private void showList()
    {
        //get the List
        getList();
        sortList();

        //actually show it
        Debug.Log("ScoreList: liste darstellen");

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;//höhe des templates, gewissermaßen zeilenhöhe
        Transform entryTransform = Instantiate(entryTemplate, container); //instanzieren von template in den Container hinein
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankString;

        float _time = highscoreEntry._time;

        //entryTransform.Find("timeText").GetComponent<TMP_Text>().text = _time.ToString();
        entryTransform.Find("timeText").GetComponent<TMP_Text>().text = $"Time: {(int)_time / 60}:{(_time) % 60:00.000} ";

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = name;

        string _car = highscoreEntry.car;
        entryTransform.Find("carText").GetComponent<TMP_Text>().text = _car;

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);//background heller bei ungeraden
        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(string _myPlanetName, float _time, string name, string car)
    {
        Debug.Log("ScoreList: AddHighscoreEntry wurde gerufen");
        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { _time = _time, name = name, car = car };

        myPlanetName = _myPlanetName;

        //Load saved Highscores
        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        getList();

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        sortList();
        resizeList();

        //Save updated Highscores
        saveList();
    }

    private void getList()
    {
        string jsonString = PlayerPrefs.GetString(myPlanetName);
        checkList(jsonString);//gibt es überhaupt eine Liste?
        jsonString = PlayerPrefs.GetString(myPlanetName);//evtl hat checkList estwas eingefuegt, jsonString muss also neu eingelesen werden.

        Debug.Log("ScoreList: in jsonString steht folgendes: " + jsonString);
        highscores = JsonUtility.FromJson<Highscores>(jsonString);//definieren
    }

    private void checkList(string jsonString)//checks if there is something in the List
    {
        if (jsonString.Length == 0)
        {
            Debug.Log("ScoreList: jsonString ist NULL!");
            SaveDefaultList();//Adds a Default List if there was no List before
        }
    }

    private void sortList()
    {
        //test how many positions
        Debug.Log("ScoreList: es gibt " + highscores.highscoreEntryList.Count + " einträge in der Liste von" + myPlanetName);

        //Sort entry by _time
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)//von anfang zu ende
        {
            Debug.Log("ScoreLIst: i hat nummer: " + i);
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                Debug.Log("ScoreLIst: j hat nummer: " + j);
                if (highscores.highscoreEntryList[j]._time < highscores.highscoreEntryList[i]._time)
                {
                    //swap
                    Debug.Log("ScoreList: jetzt wird geordnet");

                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)//Beim größten anfangen, zum kleinsten gehen
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j]._time == highscores.highscoreEntryList[i]._time && highscores.highscoreEntryList[j].name == highscores.highscoreEntryList[i].name && highscores.highscoreEntryList[j].car == highscores.highscoreEntryList[i].car)
                {
                    //if both are exactly the same
                    highscores.highscoreEntryList.RemoveAt(i);
                }
            }
        }
    }

    private void resizeList()
    {
        while (highscores.highscoreEntryList.Count > 10)//so sind nur 10 Elemente da
        {
            highscores.highscoreEntryList.Remove(highscores.highscoreEntryList[10]);
            Debug.Log("ScoreList: Nurnoch 10 eintraege in Liste von" + myPlanetName);
        }
    }

    private void saveList()
    {
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(myPlanetName, json);
        PlayerPrefs.Save();
        Debug.Log("ScoreList: SaveList wurde ausgeführt");
    }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Represents a single HighScore entry
    [System.Serializable]
    private class HighscoreEntry
    {
        public float _time;
        public string name;
        public string car;
    }

    //METHODE ZUM WARTEN ODER TESTEN!!!!!
    public void SaveDefaultList()//Einmal (1) rufen, wenn in Zeile 49 oder 132 ein NullPointer kommt!
    {
        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ _time = 550, name = "J4n4 (^.^)", car = "Volksauto"},
            new HighscoreEntry{ _time = 560, name = "L4ss3 d3r D0N", car = "Volksauto"},
            new HighscoreEntry{ _time = 570, name = "[_Fab13nn3_]", car = "Volksauto"},
            new HighscoreEntry{ _time = 580, name = "M4rv1n K1ng", car = "Volksauto"},
        };

//hier war mal n fehler, zeile 149 grueßt
        highscores = new Highscores{ highscoreEntryList = highscoreEntryList };
        saveList();
        Debug.Log("ScoreList: SaveDefaultList wurde mit der Liste: " + myPlanetName + " ausgeführt!");
    }
}
