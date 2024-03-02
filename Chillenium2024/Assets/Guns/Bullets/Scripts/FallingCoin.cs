using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().angularVelocity = 1440;
    }

    //private void Update()
    //{
    //    GetComponent<Rigidbody2D>().angularVelocity = 1440;
    //}


}
