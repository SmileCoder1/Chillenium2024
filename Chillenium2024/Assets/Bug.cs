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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                transform.position = new Vector2(-wallDisp, player.transform.position.y + ySpawnDisp);
                break;
            case 1:
                transform.position = new Vector2(-wallDisp, player.transform.position.y +  -ySpawnDisp);
                break;
            case 2:
                transform.position = new Vector2(wallDisp, player.transform.position.y +  ySpawnDisp);
                break;
            case 3:
                transform.position = new Vector2(wallDisp, player.transform.position.y +  -ySpawnDisp);
                break;
        }
        dir = (int)(Mathf.Clamp(transform.position.x, -1, 1));
        transform.localScale = new Vector3(1, 1, (int)(Mathf.Clamp(transform.position.x, -1, 1)));
        rb.velocity = Vector2.up * walkSpeed * dir;

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - waitTimer > cutWait && anchor != null)
        {
            rb.velocity = Vector2.zero;
            anchor.endEverything();
            anchor = null;
        }
        else
        {
            rb.velocity = Vector2.up * walkSpeed * dir;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Anchor") { 
            anchor = collision.gameObject.GetComponent<Anchor>();
            waitTimer = Time.time;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Anchor" && collision.gameObject == anchor) { 
            anchor = null;
            
        }
    }
}
