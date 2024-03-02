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
    public GameObject drop;
    private Vector2 accel;
    private bool accelUp;
    public enum bugType
    {
        CUT,
        CLIMB,
        FLY
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
        
        if(type == bugType.FLY)
        {
            accel = Vector2.up;
            accelUp = true;
            transform.position = new Vector2(Random.Range(-wallDisp + 1, wallDisp - 1), player.transform.position.y - ySpawnDisp);
        }
        else
        {
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

        

    }

    public void dieLogic()
    {
        dying = true;
        if(drop != null)
        {
            GameObject d = Instantiate(drop);
            d.transform.position = transform.position;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dying)
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
                        climbing = false;
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
            else if(type == bugType.FLY)
            {
                if(Random.Range(0f, 1f) < 0.2 * Time.fixedDeltaTime)
                {
                    if(Random.Range(0f, 1f) < 0.8)
                    {
                        accelUp = true;
                    }
                    else accelUp = false;
                }
                accel = new Vector2(Random.Range(-4f, 4f), accelUp ? 2f : -1f);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity + accel * Time.fixedDeltaTime, 3);
                if(transform.position.x < -wallDisp + 1 && accel.x < 0 || transform.position.x > wallDisp - 1 && accel.x > 0)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);

                }
                if(transform.position.y < player.transform.position.y && accel.y < 0|| transform.position.y > player.transform.position.y + ySpawnDisp / 2 && accel.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Roper player;
            if (collision.gameObject.TryGetComponent<Roper>(out player))
        {
            player.Suicide();
            Destroy(gameObject);
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
