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
    private int ropeCount = 10;
    public AudioClip hurt;
    public AudioClip hurt2;
    public AudioClip noRope;
    private AudioSource hurtSrc;
    private float hurtCooldown = 0;
    private float ropeCooldown = 0;
    public bool hurtBool = false;
    public bool Shootable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        hurtSrc = gameObject.AddComponent<AudioSource>();
        ropeList = new Dictionary<int, SpringJoint2D>();
        Shootable = true;

        StartCoroutine(Delay(() =>
        {
            ShootRope(transform.position + new Vector3(1, 0, 0));
            ShootRope(transform.position + new Vector3(-1, 0, 0));
        }));
        StartCoroutine(AddRopes());

    }

    IEnumerator AddRopes()
    {
        while (true)
        {
            if (ropeCount < 5)
            {
                ropeCount++;
            }
            yield return new WaitForSecondsRealtime(5f);
        }
    }

    IEnumerator Delay(System.Action a)
    {
        yield return new WaitForEndOfFrameUnit();
        a();
    }

    // Update is called once per frame
    void Update()
    {
        hurtCooldown -= Time.deltaTime;
        ropeCooldown -= Time.deltaTime;
        ropText.text = ropeCount.ToString() + " Ropes";

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Suicide();
        }

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

        


    }

    public void Suicide()
    {
        if(hurtCooldown <= 0 && hurtBool)
        {
            if(Random.Range(0f, 1f) > 0.5)
            {
                hurtSrc.clip = hurt;
            }
            else
            {
                hurtSrc.clip = hurt2;
            }
            hurtSrc.Play();
            hurtCooldown = 1;
            hurtBool = false;
        }
        foreach (var r in GetComponentsInChildren<Rope>())
        {
            r.DIE();
        }
    }
    private bool camCanSee(Vector3 point)
    {
        Camera cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Vector2 viewportPoint = cam.WorldToViewportPoint(point);
        return (new Rect(0, 0, 1, 2).Contains(viewportPoint));
    }

    private void ShootRope(Vector2 target)
    {
        hurtSrc.clip = noRope;
        Debug.Log("Target exist: " + target);
        if (ropeCount < 1)
        {
            if(ropeCooldown <= 0)
            {
                ropeCooldown = 1;
                hurtSrc.Play();
            }
            
            return;
        }
        

        Vector2 monke = this.transform.position;
        Vector2 mouse = target;

        Vector2 dir = mouse - monke;

        RaycastHit2D hit = Physics2D.Raycast(monke, dir, 1000f, (1 << LayerMask.NameToLayer("Wall")) | (1 << LayerMask.NameToLayer("NoTouch")) | (1 << LayerMask.NameToLayer("Enemy")));
        GameObject r;
        /*if(hit.rigidbody == null)
        {
            return;
        }*/
        if (hit.rigidbody == null || !camCanSee(hit.point) || hit.rigidbody.gameObject.layer != LayerMask.NameToLayer("Wall"))
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
            if(ropeCooldown <= 0)
            {
                ropeCooldown = 1;
                hurtSrc.Play();
            }
            return;
        }

        ropeCount--;

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
        ropeCount += 5;

    }

}
