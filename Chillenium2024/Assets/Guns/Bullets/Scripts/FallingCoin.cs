using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoin : MonoBehaviour
{
    public float lifeSpan = 4f;
    public float age = 0f;
    public Rigidbody2D rb;

    
    // Start is called before the first frame update
    void Start()
    {
        rb.angularVelocity = 1440;
        rb.gravityScale = 3;

        
    }

    private void Update()
    {
        age += Time.deltaTime;
        if(lifeSpan <= age)
            Destroy(gameObject);
    }


}
