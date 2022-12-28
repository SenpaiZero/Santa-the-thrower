using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameObject transition;
    public TextMeshProUGUI hScore;
    void Start()
    {
        transition = GameObject.FindGameObjectWithTag("transition");
    }

    public void setHighScore(int score)
    {
        if (PlayerPrefs.GetInt("score") < score)
        {
            hScore.text = "HIGHSCORE: " + score.ToString();
            PlayerPrefs.SetInt("score", score);
        }
        else
        {
            hScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("score");
        }
    }

    public void restart()
    {
        transition.GetComponent<Transition>().outTransition(1);
    }

    public void mainMenu()
    {
        transition.GetComponent<Transition>().outTransition(0);
    }

}
