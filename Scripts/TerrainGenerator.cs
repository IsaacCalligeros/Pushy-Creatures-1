using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public Vector3 starting;
    public Vector3 RearOffset;
    public Vector3 FrontOffset;
    private GameObject GroundPrefab;
    GameObject GroundPrefabClone;
    public GameObject[] Grounds;
    private bool spawnRear = true;
    private bool spawnFront = true;
    private GameObject GroundPrefabLocal;


    // Use this for initialization
    //Get prefab and save ground start position.
    void Start () {
        GroundPrefab = (GameObject)Resources.Load("GroundPrefab", typeof(GameObject));
        starting = GroundPrefab.transform.position;
        RearOffset = new Vector3((float)-35, 0, 0);
        FrontOffset = new Vector3((float)35, 0, 0);
    }

    //Procedural ground generation, create if ground doesn't already exist
    //Delete if a certain distance away from new spawned ground objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawnRear = true;
        spawnFront = true;
        Grounds = GameObject.FindGameObjectsWithTag("Ground");
        
        for (int i = 0; i < Grounds.Length; i++)
        {
            if (Grounds[i].transform.position.x - transform.parent.position.x > 50 || Grounds[i].transform.position.x - transform.parent.position.x < -50)
            {
                Destroy(Grounds[i]);
            }
            else if (Grounds[i].transform.position.x == transform.parent.position.x + RearOffset.x)
            {
                spawnRear = false;
            }
            else if  (Grounds[i].transform.position.x == transform.parent.position.x + FrontOffset.x)
            {
                spawnFront = false;
            }
        }

        if (gameObject.name == "FrontCollider" && spawnFront)
        {

            GroundPrefabClone = Instantiate(GroundPrefab, new Vector3(transform.parent.position.x + FrontOffset.x, (float)-1.5, 0), Quaternion.identity) as GameObject;
            GroundPrefabClone.name = "GroundPrefabClone" + GroundPrefabClone.transform.position.x;
            GroundPrefabClone.tag = "Ground";
        }
        else if (gameObject.name == "RearCollider" && spawnRear)
        {
           
            GroundPrefabClone = Instantiate(GroundPrefab, new Vector3(transform.parent.position.x + RearOffset.x, (float)-1.5, 0), Quaternion.identity) as GameObject;
            GroundPrefabClone.name = "GroundPrefabClone" + GroundPrefabClone.transform.position.x;
            GroundPrefabClone.tag = "Ground";
        }
    }
}
