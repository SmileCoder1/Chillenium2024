using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var v = transform.rotation.eulerAngles;
        v += new Vector3(0, 0, Mathf.Sin(Time.fixedTime + Mathf.PI/4) / Mathf.PI / 10);
        transform.rotation = Quaternion.Euler(v);
    }
}
