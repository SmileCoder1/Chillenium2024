using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Roper : MonoBehaviour
{

    public GameObject rope;
    private Dictionary<int, SpringJoint2D> ropeList;
    private int c = 0;
    private float deltaRope = .01f;
     public bool Shootable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ropeList = new Dictionary<int, SpringJoint2D>();
        Shootable = true;
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


        if (Input.GetKeyDown(KeyCode.Mouse1) && Shootable) {

            Vector2 monke = this.transform.position;
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2 dir = mouse - monke;

            RaycastHit2D hit = Physics2D.Raycast(monke, dir, 1000f, 1 << LayerMask.NameToLayer("Wall"));
            
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
            
            GameObject r = Instantiate(rope);
            r.transform.parent = gameObject.transform;
            r.GetComponent<Rope>().anchor_world_point = hit.point;

            ropeList.Add(c, sj);
            r.GetComponent<Rope>().id = c++;

        }
    }

    public void RemoveRope(int id)
    {
        Destroy(ropeList[id]);
        ropeList.Remove(id);
    }

    public void CompressRope(int id)
    {
        ropeList[id].distance -= deltaRope;
        foreach (var c in ropeList)
        {
            if (c.Key == id) continue;
            c.Value.distance += deltaRope / 2;
        }
    }
    
}