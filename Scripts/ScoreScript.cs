using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    Text score;
    public GameObject highScoreTextObj;
    Text highScore;

    void Start() {
        //PlayerPrefs.DeleteAll();
        score = GetComponent<Text>();
        highScore = highScoreTextObj.GetComponent<Text>();
        highScore.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString("0.00");
        score.enabled = false;
        highScoreTextObj.SetActive(true);
    }

    void FixedUpdate() {
        if (!float.IsNaN(CreatureCamera.TheAverage))
        {
            score.text = "Distance: " + CreatureCamera.TheAverage.ToString("0.00");
        }
        else {
            score.text = "Distance: " + "0.00";

        }
    }

    public void GameEnd()
    {
        if (CreatureCamera.TheAverage > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", CreatureCamera.TheAverage);
            highScore.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString("0.00");
            score.enabled = false;
            highScoreTextObj.SetActive(true);
        }
    }
}
