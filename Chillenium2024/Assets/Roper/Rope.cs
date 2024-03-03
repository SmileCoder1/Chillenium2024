using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Vector2 anchor_world_point;
    public int id;
    public int colScale = 5;
    private LineRenderer lr;
    public LineRenderer highlighter;
    private MeshCollider meshCollider;
    private GameObject anchorRef;
    private bool dying;
    private Vector3 retractPoint;
    public float retractTime;
    private float retractStart;
    //private bool dying;

    [SerializeField]
    private GameObject anchor;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        transform.localScale = Vector3.one * colScale;

        highlighter = new GameObject().AddComponent<LineRenderer>() as LineRenderer;
        highlighter.transform.parent = transform;
        highlighter.useWorldSpace = false;
        highlighter.startWidth = highlighter.endWidth = .25f * colScale / 2;
        highlighter.material = new Material(Shader.Find("Sprites/Default"));
        highlighter.startColor = Color.white;
        highlighter.endColor = Color.white;
        highlighter.material.color = new Color(1, 0, 0, .25f);
        highlighter.enabled= false;
        highlighter.sortingOrder = -1;


        meshCollider = this.AddComponent<MeshCollider>();

        Mesh m = new Mesh();
        lr.BakeMesh(m);
        meshCollider.sharedMesh = m;
        if (!dying)
        {
            GameObject a = Instantiate(anchor);
            anchorRef = a;
            a.transform.position = anchor_world_point;
            
            a.GetComponent<Anchor>().parent = this;
            a.GetComponent<Anchor>().sfx();
        }
        retractPoint = anchor_world_point;
        

    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.InverseTransformPoint(transform.parent.position));
        if (!dying)
        {
            lr.SetPosition(1, transform.InverseTransformPoint(anchor_world_point));
            highlighter.SetPosition(1, highlighter. transform.InverseTransformPoint(anchor_world_point));
        }
        else
        {
            if(Time.time - retractStart > retractTime)
            {
                Destroy(gameObject);
            }
            else
            {
                retractPoint = anchor_world_point + ((Vector2)transform.parent.position - anchor_world_point) * (Time.time - retractStart) / retractTime;
                lr.SetPosition(1, transform.InverseTransformPoint(retractPoint));
                highlighter.SetPosition(1, highlighter. transform.InverseTransformPoint(retractPoint));
            }
            
        }
        lr.enabled = true;

        highlighter.SetPosition(0, highlighter.transform.InverseTransformPoint(transform.parent.position));
        
        Mesh m = new Mesh();
        lr.BakeMesh(m);
        lr.sortingLayerName = "Player";
        meshCollider.sharedMesh = m;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) {
            DIE();
            
        }
    }

    private void OnDestroy()
    {
        transform.parent.gameObject.GetComponent<Roper>().Shootable = true;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Roper On Mouse Enter");
        transform.parent.gameObject.GetComponent<Roper>().Shootable = false;
        highlighter.enabled = true;
    }
    private void OnMouseExit()
    {
        Debug.Log("Roper On Mouse Exit");
        transform.parent.gameObject.GetComponent<Roper>().Shootable = true;
        highlighter.enabled = false;
    }

    public void DIE()
    {
        if(id != -1) transform.parent.gameObject.GetComponent<Roper>().RemoveRope(id);
        if (anchorRef != null)
        {
            Debug.Log("anchor yeet");
            Destroy(anchorRef);
            if (anchorRef == null)
            {
                Debug.Log("anchor yeet success");
            }
        }
        dying = true;
        retractStart = Time.time;
    }
}
