using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointJoiner : MonoBehaviour
{
    public GameObject PrefabObj;
    public bool joints;
    public bool second;

    GameObject Node1;
    GameObject Node2;
    GameObject TheJoint;
    private Vector3 AttatchmentVector;
    Vector3[] ends = new Vector3[2];
    Vector3[] BaseJointends = new Vector3[2];
    RaycastHit hit;
    private LineRenderer linerenderer;
    LineRenderer colorRenderer;
    GameObject MainCamera;

    float thedistance;
    double currLength = 7;
    int counter = 0;
    public float JoinerForce = 200;
    public float tempforce;



    //Get Camera and joint color, stop joint placement
    void Start()
    {
        joints = false;
        second = false;
        colorRenderer = GameObject.Find("JointLineRenderer").GetComponent<LineRenderer>();
        MainCamera = GameObject.Find("Main Camera");
    }

    // links joint color to slider
    public void AdjustForce(float JoinerForce)
    {
        tempforce = JoinerForce;
        Color m_color = Color.Lerp(new Color((float)1, (float)0.97, (float)0.86), new Color((float)0.66, (float)0.27, (float)0.075), (tempforce - 200) / 200);
        m_color.a = (float)0.784;
        colorRenderer.SetColors(m_color, m_color);
    }


    // Check for Nodes and link joints, link joint position to camera to follow for placement, placement blocked above certain distance anyway.
    void Update()
    {     
        NodeFind();
        transform.position = new Vector3(MainCamera.transform.position.x, 0, 0);
        BaseJointends[0]= new Vector3(MainCamera.transform.position.x + 3, -1, 0); 
        BaseJointends[1] = new Vector3(MainCamera.transform.position.x + 5, -1, 0);
        colorRenderer.SetPositions(BaseJointends);
    }

    /* Gets first Node clicked after Joint instantiation, cancel joint on game objects that cant be linked.
     * Then second Node, cancel on same conditions, places the joint
     * Instantiates the MyJoint object with all the variables set in NodeFind()
      */
    private void NodeFind()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //second node clicked, Destroy if it's basenode
            if (second)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider.gameObject.name == "BaseNode")
                {
                    Destroy(TheJoint);
                    joints = false;
                    second = false;
                    second = false;
                    Node1 = null;
                    Node2 = null;
                }
                // Get the node, add Joint object, set joint object variables, and draw reset joints variables.
                if ((hit.collider != null) && (hit.collider.gameObject.name != Node1.name) && (!hit.collider.gameObject.name.Contains("Joint")) && (hit.collider.gameObject.name.Contains("Clone")))
                {
                    Node2 = hit.collider.gameObject;
                    Node1.AddComponent<MyJoint>();

                    MyJoint[] allBaseJoints = Node1.GetComponents<MyJoint>();
                    allBaseJoints[allBaseJoints.Length - 1].scriptVer = allBaseJoints.Length - 1;
                    int tempoLength = allBaseJoints.Length - 1;

                    for (int i = 0; i < allBaseJoints.Length-1; i++)
                    {
                        Debug.Log(Node1.name);
                        Debug.Log(Node2.name);
                        if (allBaseJoints[i].Sprite1 == Node1)
                        {
                            if (allBaseJoints[i].Sprite2 == Node2)
                            {
                                Destroy(TheJoint);
                                return;
                            }
                        }
                    }
                
                   allBaseJoints[tempoLength].Sprite1 = Node1;
                   allBaseJoints[tempoLength].Sprite2 = Node2;
                   allBaseJoints[tempoLength].Line = TheJoint;
                   allBaseJoints[tempoLength].force = tempforce;

                    // Draws during pause
                    AttatchmentVector = Node1.transform.position - Node2.transform.position;
                    AttatchmentVector.z = 0;
                    currLength = ((AttatchmentVector.x * AttatchmentVector.x) + (AttatchmentVector.y * AttatchmentVector.y) / 2);
                    linerenderer.endWidth = 3 / (float)currLength;
                    linerenderer.startWidth = 3 / (float)currLength;
                    if (linerenderer.endWidth > 1)
                        {
                        linerenderer.endWidth = 1;
                        linerenderer.startWidth = 1;
                         }
                    ends[0] = Node1.transform.position;
                    ends[1] = Node2.transform.position;
                    linerenderer.SetPositions(ends);

                    joints = false;
                    second = false;
                }
            }

            //Set first node clicked, set variable for first node sleected to true
            if (joints)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    if (!hit.collider.gameObject.name.Contains("Joint") && (hit.collider.gameObject.name.Contains("Clone")))
                    {
                        Node1 = hit.collider.gameObject;
                        second = true;
                    }
                }
            }

        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.name.Contains("Joint") && (!gameObject.name.Contains("Clone")))
            {
                if ((!second) && (!joints))
                {
                    joints = true;
                    second = false;
                    TheJoint = Instantiate(PrefabObj, transform.position, Quaternion.identity);
                    Color m_color = Color.Lerp(new Color((float)1, (float)0.97, (float)0.86), new Color((float)0.66, (float)0.27, (float)0.075), (tempforce - 200) / 200);
                    m_color.a = (float)0.784;
                    colorRenderer.SetColors(m_color, m_color);
                    TheJoint.name = TheJoint.name + counter;
                    TheJoint.tag = "spawnedjoints";
                    linerenderer = TheJoint.GetComponent<LineRenderer>();           
                    Destroy(TheJoint.GetComponent<JointJoiner>());
                    Destroy(TheJoint.GetComponent<BoxCollider2D>());
                    counter++;
                }
                else if (second)
                {
                    Destroy(TheJoint);
                    joints = false;
                    second = false;
                    Node1 = null;
                    Node2 = null;
                }
                else if (joints)
                {
                    Destroy(TheJoint);
                    joints = false;
                    second = false;
                }
            }
        }
    }
}
