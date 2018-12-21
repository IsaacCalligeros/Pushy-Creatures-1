using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyJoint : MonoBehaviour {

    private Rigidbody2D attatchedSprite;
    public GameObject Sprite1;

    private Rigidbody2D connectedSprite;
    public GameObject Sprite2;

    public GameObject Line;
    private LineRenderer linerenderer;

    public float MaxLength=11;
    public double currLength=7;
    public float MinLength=5;
    public float force=200;
    public float centre = 0.5f;
    public bool flag = true;
    private Vector3 AttatchmentVector;
    Vector3[] ends = new Vector3[2];
    public int scriptVer = 0;



	// Use this for initialization
	void Start () {
        attatchedSprite = Sprite1.GetComponent<Rigidbody2D>();
        connectedSprite = Sprite2.GetComponent<Rigidbody2D>();
        linerenderer = Line.GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate () {
        AttatchmentVector = Sprite1.transform.position - Sprite2.transform.position;
        AttatchmentVector.z = 0;


        currLength = (Math.Pow(AttatchmentVector.x, 2) + Math.Pow(AttatchmentVector.y, 2) / 2);
        // Debug.Log(currLength);

        if (currLength >= MaxLength*5/6)
        {
            flag = true;
        }
        else if ((currLength <= MinLength*1.5))
        {
            flag = false;
        }
   

        //pull
            if (flag)
        {
            connectedSprite.AddForce(AttatchmentVector * Time.deltaTime * force * centre);
            attatchedSprite.AddForce(-AttatchmentVector * Time.deltaTime * force *(1-centre));
        }
            //push
        else if (!flag)
        {
            connectedSprite.AddForce(-AttatchmentVector * Time.deltaTime * force * centre);
            attatchedSprite.AddForce(AttatchmentVector * Time.deltaTime * force * (1-centre));
        }

            //Setting velocity to zero, messes up gravity 
        if (currLength <= MinLength )
        {
          //  Debug.Log("Min");
        }

        else if ( currLength >= MaxLength)
        {
          //  Debug.Log("Max");
        }

        // Draw Connecting line
        ends[0] = Sprite1.transform.position;
        ends[1] = Sprite2.transform.position;
        linerenderer.endWidth =  3/(float)currLength;
        linerenderer.startWidth = 3/(float)currLength;
        if (linerenderer.endWidth > 1)
        {
            linerenderer.endWidth = 1;
            linerenderer.startWidth = 1;
        }
        linerenderer.SetPositions(ends);
    }
}
