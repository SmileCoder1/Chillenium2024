using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheating : MonoBehaviour
{
    private Rigidbody2D monky;
    // Start is called before the first frame update
    void Start()
    {
        monky = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            monky.velocity = monky.velocity.x *Vector2.right + 12*Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            monky.velocity = monky.velocity.y * Vector2.up + 12 * Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            monky.velocity = monky.velocity.x * Vector2.right + 12 * Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            monky.velocity = monky.velocity.y * Vector2.up + 12 * Vector2.right;
        }

        if (Input.GetKeyUp(KeyCode.W)) { 
            monky.velocity = monky.velocity.x *Vector2.right;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            monky.velocity = monky.velocity.y * Vector2.up;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            monky.velocity = monky.velocity.x * Vector2.right;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            monky.velocity = monky.velocity.y * Vector2.up;
        }
    }
}
