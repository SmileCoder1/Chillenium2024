using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float wallDisp; // wall displacement from center
    public float ySpawnDisp;
    public float walkSpeed;
    public float cutWait; // wait time before cutting rope
    private int dir;
    private GameObject player;
    private Rigidbody2D rb;
    private float waitTimer;
    private Anchor anchor;
    private bool dying;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        int i = Random.Range(0, 4);
        dir = 1;
        switch (i)
        {
           
            
            case 0:
                transform.position = new Vector2(-wallDisp, player.transform.position.y + ySpawnDisp);
                dir = -1;
                break;
            case 1:
                transform.position = new Vector2(-wallDisp, player.transform.position.y +  -ySpawnDisp);
                break;
            case 2:
                transform.position = new Vector2(wallDisp, player.transform.position.y +  ySpawnDisp);
                dir = -1;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case 3:
                transform.position = new Vector2(wallDisp, player.transform.position.y +  -ySpawnDisp);
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rb.velocity = Vector2.up * walkSpeed * dir;

    }

    public void dieLogic()
    {
        dying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - waitTimer > cutWait && anchor != null)
        {
            rb.velocity = Vector2.zero;
            anchor.endEverything();
            anchor = null;
            rb.velocity = Vector2.up * walkSpeed * dir;
        }
        else if(anchor == null && !dying)
        {
            rb.velocity = Vector2.up * walkSpeed * dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bug touched something");
        if (collision.gameObject.tag == "Anchor") { 
            anchor = collision.gameObject.GetComponent<Anchor>();
            waitTimer = Time.time;
            rb.velocity = Vector2.zero;
            Debug.Log("enter anchor: " + anchor.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Anchor" && collision.gameObject.GetComponent<Anchor>() == anchor) { 
            anchor = null;
            Debug.Log("exit anchor");
            
        }
    }
}
