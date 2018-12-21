using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeleteClones : MonoBehaviour {
    public Button DeleteButton;
    GameObject[] clones;
    LineRenderer linerenderer;
    GameObject MyTimer;
    private GameObject[] groundObjects;

    void Start()
    {
        DeleteButton.onClick.AddListener(TaskOnClick);
        MyTimer = GameObject.Find("TimeButton");
    }


    public void TaskOnClick()
    {
        DestroyClones();
        GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
        ResetGround();

        //Reset timer and pause if running, else just reset if paused.
        if (MyTimer.GetComponent<GameTimer>().timer == true)
        {
            MyTimer.GetComponent<GameTimer>().timer = true;
            MyTimer.GetComponent<GameTimer>().TaskOnClick();
        }
        MyTimer.GetComponent<GameTimer>().ResetTime();
    }
    
    // Removes all instantiated clone and joint objects
    void DestroyClones()
    {
        var clones = GameObject.FindGameObjectsWithTag("Clone");
        var alljoints = GameObject.FindGameObjectsWithTag("spawnedjoints");

        for (int i = 0; i < clones.Length; i++)
        {
            Destroy(clones[i]);
        }
        for (int j = 0; j < alljoints.Length; j++)
        {
            Destroy(alljoints[j]);

        }
        MyTimer.GetComponent<GameTimer>().ResetTime();
    }

    // Deletes all ground objects, instantiates initial ground location.
    void ResetGround()
    {
        GameObject Ground = (GameObject)Resources.Load("GroundPrefab", typeof(GameObject));
        groundObjects = GameObject.FindGameObjectsWithTag("Ground");
        for (int j = 0; j < groundObjects.Length; j++)
        {
            Destroy(groundObjects[j]);
        }
        Instantiate(Ground, new Vector3(-10, (float)-1.5, 0), Quaternion.identity);
    }
}
