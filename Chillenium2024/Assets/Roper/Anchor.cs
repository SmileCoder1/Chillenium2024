using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public Rope parent;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endEverything()
    {
        parent.DIE();
        Destroy(this.gameObject);
    }
}
