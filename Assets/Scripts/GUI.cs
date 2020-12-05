using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{

    private float highscore;
    private float currentScore;
    public Button buttonPlay;

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetFloat("highscore");
        currentScore = PlayerPrefs.GetFloat("currentscore");
        GetComponent<TextMeshProUGUI>().SetText("Current Score :\n"+ (int)currentScore+ "\nHighest Score :\n"+(int)highscore);
        buttonPlay.onClick.AddListener(TaskOnClick);
    }
    private void TaskOnClick()
    {
        SceneManager.LoadScene("Prototype_Tower", LoadSceneMode.Single);
    }
}
