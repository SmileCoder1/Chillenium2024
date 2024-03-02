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
    private LineRenderer lr;
    private MeshCollider meshCollider;
    private GameObject anchorRef;

    [SerializeField]
    private GameObject anchor;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.SetPosition(0, transform.InverseTransformPoint( transform.parent.position));
        lr.SetPosition(1, transform.InverseTransformPoint(anchor_world_point));

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
        Mesh m = new Mesh();
        lr.BakeMesh(m);
        meshCollider.sharedMesh = m;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) {
            DIE();
            if(anchorRef != null)
            {
                Debug.Log("anchor yeet");
                DestroyImmediate(anchorRef);
                if(anchorRef == null)
                {
                    Debug.Log("anchor yeet success");
                }
            }
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
    }
    private void OnMouseExit()
    {
        Debug.Log("Roper On Mouse Exit");
        transform.parent.gameObject.GetComponent<Roper>().Shootable = true;
    }

    public void DIE()
    {
        Destroy(gameObject);
        transform.parent.gameObject.GetComponent<Roper>().RemoveRope(id);
    }
}
