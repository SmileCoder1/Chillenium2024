using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health;
    private Rigidbody2D rb;
    private float dieTimer;
    private bool dying;
    public float dieTime;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            if(Time.time - dieTimer > dieTime)
            {
                Destroy(gameObject);
            }
            else
            {
                float colorVal = 1 - (Time.time - dieTimer) / dieTime;
                sr.color = new Color(colorVal, colorVal, colorVal, colorVal);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bug got hit");
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            health--;
            if(health <= 0)
            {
                dying = true;
                dieTimer = Time.time;
                rb.isKinematic = false;
                rb.gravityScale = 3;
            }
        }
    }
}
