using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCamera : MonoBehaviour
{

    float total = 0;
    public static float TheAverage = 0;
    public GameObject TheTimer;
    public GameTimer flagtracker;
    Vector3 StartPos;


    // Use this for initialization
    void Start()
    {
        //Initalise timer for pause/play check.
        TheTimer = GameObject.Find("TimeButton");
        flagtracker = TheTimer.GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraFollow();
    }

    //Follows the average position of all clones for camera position.
    void CameraFollow()
    {
        var clones = GameObject.FindGameObjectsWithTag("Clone");
        if (clones.Length != 0)
        {
            total = 0;
            for (int i = 0; i < clones.Length; i++)
            {
                float temp = clones[i].transform.position.x;
                total += temp;
            }

            TheAverage = total / clones.Length;

            // if game paused leave camera fixed.
            if (flagtracker.timer)
            {
                GameObject.Find("Main Camera").transform.position = new Vector3(TheAverage, 0, -10);
            }
        }
    }
}

