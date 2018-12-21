using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public Button TheButton;
    public bool timer = true;
    public float timeLeft = 20;
    Text my_text;
    ScoreScript ScoreScriptObj;

    // settingactive/unactive
    GameObject Node;
    GameObject Joint;
    GameObject Sliders;
    GameObject score;
    GameObject highScoreTextObj;


    // Use this for initialization
    void Start () {
        //get game objects
        Node = GameObject.Find("BaseNode");
        Joint = GameObject.Find("JointLineRenderer");
        score = GameObject.Find("Score");
        highScoreTextObj = GameObject.Find("highScore");
        Sliders = GameObject.FindWithTag("sliders");
        my_text = GameObject.Find("Timer").GetComponent<Text>();
        ScoreScriptObj = GameObject.Find("Score").GetComponent<ScoreScript>();
        //default pause
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.0f;
        // Initialise Click
        TheButton.onClick.AddListener(TaskOnClick);
        TaskOnClick();
    }

    //Called from other classes to just reset timer
    public void ResetTime()
    {
        timeLeft = 20;
        my_text.text = (timeLeft.ToString("0.0"));
        score.GetComponent<Text>().enabled = false;
        highScoreTextObj.SetActive(true);
        Node.SetActive(true);
        Joint.SetActive(true);
        Sliders.SetActive(true);
        timer = true;
        TaskOnClick();
    }

    //game running, reduce time, update text and reset if time up.
    void Update()
    {
            if (timer)
            {
                timeLeft -= Time.deltaTime;
                my_text.text = (timeLeft.ToString("0.0"));
                if (timeLeft < 0)
                {
                ScoreScriptObj.GameEnd();
                score.GetComponent<Text>().enabled = false;
                highScoreTextObj.SetActive(true);
                TaskOnClick();
                }
        }
    }

    public void TaskOnClick()
    {
        // if running pause, display objects to make creature
        if (timer)
        {
            Time.timeScale = 0.0f;
            Time.fixedDeltaTime = 0.0f;
            timer = false;
            Node.SetActive(true);
            Joint.SetActive(true);
            Sliders.SetActive(true);

        }
        // if paused start running, hide objects for creature creation
        else 
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.04f;
            timer = true;
            Node.SetActive(false);
            Joint.SetActive(false);
            Sliders.SetActive(false);
            score.GetComponent<Text>().enabled = true;
            highScoreTextObj.SetActive(false);
        }
    }
}
