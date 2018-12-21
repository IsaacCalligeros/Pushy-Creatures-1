using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDrop : MonoBehaviour
{

    private bool selected;
    public GameObject PrefabObj;
    GameObject PrefabObjClone;
    Vector2 cursorPos;
    public int counter = 0;
    Rigidbody2D RigidBodyClone;
    Rigidbody2D RigidBodyOG;
    public GameObject TheJoiner;
    public JointJoiner JointsTracker;
    SpriteRenderer colorRenderer;
    GameObject MainCamera;

    // Use this for initialization
    void Start()
    {
        RigidBodyOG = GetComponent<Rigidbody2D>();
        TheJoiner = GameObject.Find("JointLineRenderer");
        JointsTracker = TheJoiner.GetComponent<JointJoiner>();
        colorRenderer = GameObject.Find("BaseNode").GetComponent<SpriteRenderer>();
        MainCamera = GameObject.Find("Main Camera");
    }

    public void AdjustMass(float newmass)
    {
        RigidBodyOG.mass = newmass;
        colorRenderer.material.color = Color.Lerp(Color.yellow, Color.red, RigidBodyOG.mass - 1);
    }


    public void AdjustGravity(float newGravity)
    {
        RigidBodyOG.gravityScale = newGravity;

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3 (MainCamera.transform.position.x + (float)4, 2, 0);
        if (selected)
       {
            cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PrefabObjClone.transform.position = new Vector2(cursorPos.x, cursorPos.y);

        }
        //Mouse release place object, destroy if too close to initial
        if (Input.GetMouseButtonUp(0))
        {
            if ((cursorPos.x > 3.8) || (cursorPos.y < -2.7))
            {
                Object.Destroy(PrefabObjClone);
            }
            selected = false;
        }
     }

    private void OnMouseOver()
    {
        //Clones cant instantiate new Obj
        //Get current Position, instantiate clone, sel true for positioning.
        if (Input.GetMouseButtonDown(0))
        {
                if (!JointsTracker.joints && !JointsTracker.second)
                {
                    if (!gameObject.name.Contains("Joint") && (!gameObject.name.Contains("Clone")))
                    {
                        PrefabObjClone = Instantiate(PrefabObj, transform.position, Quaternion.identity);
                        PrefabObjClone.name = PrefabObjClone.name + counter;
                        PrefabObjClone.tag = "Clone";
                        RigidBodyClone = PrefabObjClone.GetComponent<Rigidbody2D>();

                        PrefabObjClone.GetComponent<SpriteRenderer>().material = Instantiate<Material>(GetComponent<SpriteRenderer>().material);
                        colorRenderer.material.color = Color.Lerp(Color.yellow, Color.red, RigidBodyOG.mass - 1);

                        RigidBodyClone.constraints = RigidbodyConstraints2D.None;
                        Destroy(PrefabObjClone.GetComponent<DragDrop>());
                        selected = true;
                        counter++;
                    }
            }
        }
    }
}


