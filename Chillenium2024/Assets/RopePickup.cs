using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePickup : MonoBehaviour
{
    public float lifetime;
    public float movespeed;
    private float start;
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    // Update is called once per frame

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        start = Time.time;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        var v = transform.rotation.eulerAngles;
        v    += new Vector3(0,0, Mathf.Cos(Time.fixedTime) / Mathf.PI / 2);
        transform.rotation = Quaternion.Euler(v);

       rb.velocity = movespeed * (player.transform.position - this.transform.position).normalized;

       if(Time.time - start > lifetime - 2)
        {
            sr.color = new Color(1, 1, 1, 0.5f);
        }
       if(Time.time - start > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Roper player;
        if(collision.gameObject.TryGetComponent(out player))
        {
            player.AddRope();
            Destroy(gameObject);
        }
    }
}