using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoin : MonoBehaviour
{
    public float lifeSpan = 1.5f;
    public float age = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void Update()
    {
        age += Time.deltaTime;
        if(lifeSpan <= age)
            Destroy(gameObject);
    }


}
