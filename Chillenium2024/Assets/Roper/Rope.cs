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

    [SerializeField]
    private GameObject anchor;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.useWorldSpace = false;
        lr.SetPosition(0, transform.InverseTransformPoint( transform.parent.position));
        lr.SetPosition(1, transform.InverseTransformPoint(anchor_world_point));
        lr.enabled = true;
        transform.localScale = Vector3.one * colScale;

        highlighter = new GameObject().AddComponent<LineRenderer>() as LineRenderer;
        highlighter.transform.parent = transform;
        highlighter.useWorldSpace = false;
        highlighter.SetPosition(0, highlighter.transform.transform.InverseTransformPoint(transform.parent.position));
        highlighter.SetPosition(1, highlighter.transform.transform.InverseTransformPoint(anchor_world_point));
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
        GameObject a = Instantiate(anchor);
        anchorRef = a;
        a.transform.position = anchor_world_point;
        a.GetComponent<Anchor>().parent = this;

    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.InverseTransformPoint(transform.parent.position));
        lr.SetPosition(1, transform.InverseTransformPoint(anchor_world_point));
        highlighter.SetPosition(0, highlighter.transform.InverseTransformPoint(transform.parent.position));
        highlighter.SetPosition(1, highlighter. transform.InverseTransformPoint(anchor_world_point));
        Mesh m = new Mesh();
        lr.BakeMesh(m);
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
        Destroy(gameObject);
        transform.parent.gameObject.GetComponent<Roper>().RemoveRope(id);
        if (anchorRef != null)
        {
            Debug.Log("anchor yeet");
            Destroy(anchorRef);
            if (anchorRef == null)
            {
                Debug.Log("anchor yeet success");
            }
        }
    }
}
