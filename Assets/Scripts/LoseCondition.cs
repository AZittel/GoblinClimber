using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour
{
    public float currentScore = 0;

    public GameObject canvas;
    public Transform playerTransform;
    public GameObject scoreTracker;

    public void EndGame()
    {
        PlayerPrefs.SetFloat("currentscore", currentScore);
        if(currentScore > PlayerPrefs.GetFloat("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", currentScore);
        }
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Update()
    {
        currentScore = playerTransform.position.y + Time.realtimeSinceStartup;
        scoreTracker.GetComponent<TextMeshProUGUI>().SetText("Score : " + (int)currentScore);
    }
}
