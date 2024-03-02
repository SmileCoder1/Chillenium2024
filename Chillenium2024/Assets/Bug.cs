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
    public bugType type;
    private float ropePoint;
    private bool climbing;
    public enum bugType
    {
        CUT,
        CLIMB
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        int i = Random.Range(0, 4);
        dir = 1;
        climbing = false;
        dying = false;
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
    void FixedUpdate()
    {
        if(type == bugType.CUT)
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
        else if(type == bugType.CLIMB)
        {
            if (climbing)
            {
                if(anchor == null)
                {
                    Debug.Log("bug died lol");
                    GetComponent<Entity>().startDying();
                }
                else if(!dying)
                {
                    Vector3 playerLook = (player.transform.position - anchor.transform.position).normalized;
                    rb.velocity = walkSpeed * playerLook;
                    ropePoint += walkSpeed * Time.fixedDeltaTime / (player.transform.position - anchor.transform.position).magnitude;
                    transform.position = anchor.transform.position + ropePoint * (player.transform.position - anchor.transform.position);
                    if(ropePoint >= 1)
                    {
                        
                    }
                }
            }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(type == bugType.CUT)
        {
            if (collision.gameObject.tag == "Anchor") { 
                anchor = collision.gameObject.GetComponent<Anchor>();
                waitTimer = Time.time;
                rb.velocity = Vector2.zero;
                
            }
        }
        else if(type == bugType.CLIMB)
        {
            if (collision.gameObject.tag == "Anchor") { 
                anchor = collision.gameObject.GetComponent<Anchor>();
                ropePoint = 0;
                climbing = true;
                Debug.Log("enter anchor: " + anchor.gameObject);
            }
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (type == bugType.CUT)
        {
            if (collision.gameObject.tag == "Anchor" && collision.gameObject.GetComponent<Anchor>() == anchor)
            {
                anchor = null;
                Debug.Log("exit anchor");

            }
        }
        else if (type == bugType.CLIMB)
        {
            if (collision.gameObject.tag == "Anchor" && collision.gameObject.GetComponent<Anchor>() == anchor && !climbing)
            {
                anchor = null;
                Debug.Log("exit anchor");

            }
        }
    }
}
