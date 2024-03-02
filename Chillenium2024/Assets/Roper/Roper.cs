using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Roper : MonoBehaviour
{

    public GameObject rope;
    private Dictionary<int, SpringJoint2D> ropeList;
    private int c = 0;
    private float deltaRope = .02f;
    private int shotRopeLastUpdate = -1;
    [SerializeField]
    private TMP_Text ropText;
    [SerializeField]
    private int ropeCount = 700;
    public bool Shootable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ropeList = new Dictionary<int, SpringJoint2D>();
        Shootable = true;

        ShootRope(transform.position + transform.right);
        ShootRope(transform.position - transform.right);

    }

    // Update is called once per frame
    void Update()
    {

        // mouse position is inside any child coliders
        foreach (SpringJoint2D s in ropeList.Values)
        {
            var len = (transform.position - s.connectedBody.transform.TransformPoint(s.connectedAnchor)).magnitude;
            s.enabled = s.distance <= len;
        }

        if (shotRopeLastUpdate != -1 && Input.GetMouseButton(1))
        {
            CompressRope(shotRopeLastUpdate);
            return;
        } else
        {
            shotRopeLastUpdate = -1;
        }


        if (Input.GetKeyDown(KeyCode.Mouse1) && Shootable) {

            ShootRope(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Suicide();
        }


    }

    public void Suicide()
    {
        foreach (var r in GetComponentsInChildren<Rope>())
        {
            r.DIE();
        }
    }
    private bool camCanSee(Vector3 point)
    {
        Camera cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Vector2 viewportPoint = cam.WorldToViewportPoint(point);
        return (new Rect(0, 0, 1, 1).Contains(viewportPoint));
    }

    private void ShootRope(Vector2 target)
    {
        Debug.Log("Target exist: " + target);
        if (ropeCount < 1)
        {
            return;
        }
        ropeCount--;
        //ropText.text = ropeCount.ToString() + " Ropes";

        Vector2 monke = this.transform.position;
        Vector2 mouse = target;

        Vector2 dir = mouse - monke;

        RaycastHit2D hit = Physics2D.Raycast(monke, dir, 1000f, 1 << LayerMask.NameToLayer("Wall"));
        GameObject r;
        /*if(hit.rigidbody == null)
        {
            return;
        }*/
        if (hit.rigidbody == null || !camCanSee(hit.point))
        {
            r = Instantiate(rope);
            r.transform.parent = gameObject.transform;
            Rope rp = r.GetComponent<Rope>();
            r.GetComponent<Rope>().id = -1;
            r.GetComponent<LineRenderer>().startColor = Color.red;
            r.GetComponent<LineRenderer>().endColor = Color.red;
            Debug.Log("Target still exist: " + mouse);
            rp.anchor_world_point = mouse;
            rp.DIE();
            return;
        }
       
        else if(hit.collider.gameObject.tag == "NoTouch")
        {
            r = Instantiate(rope);
            r.transform.parent = gameObject.transform;
            Rope rp = GetComponent<Rope>();
            r.GetComponent<Rope>().id = c++;
            r.GetComponent<LineRenderer>().startColor = Color.red;
            r.GetComponent<LineRenderer>().endColor = Color.red;
            rp.anchor_world_point = hit.point;
            rp.DIE();
            return;
            
        }

        // Add a component
        SpringJoint2D sj = this.AddComponent<SpringJoint2D>();
        sj.anchor = Vector2.zero;
        sj.connectedBody = hit.rigidbody;
        sj.enableCollision = true;
        sj.connectedAnchor = hit.rigidbody.transform.InverseTransformPoint(hit.point);
        sj.autoConfigureDistance = false;
        sj.distance = (hit.point - monke).magnitude / 2f;
        sj.dampingRatio = .99f;
        sj.frequency = 1f;

        r = Instantiate(rope);
        
        r.transform.parent = gameObject.transform;
        r.GetComponent<Rope>().anchor_world_point = hit.point;

        ropeList.Add(c, sj);
        r.GetComponent<Rope>().id = c++;

        shotRopeLastUpdate = c - 1;
    }

    public void RemoveRope(int id)
    {
        Destroy(ropeList[id]);
        ropeList.Remove(id);
    }

    public void CompressRope(int id)
    {
        ropeList[id].distance -= deltaRope;
        //foreach (var c in ropeList)
        //{
        //    if (c.Key == id) continue;
        //    c.Value.distance += deltaRope / 2;
        //}
    }

    public void AddRope()
    {
        ropeCount++;
        ropText.text = ropeCount.ToString() + " Ropes";

    }

}
