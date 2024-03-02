using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePickup : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var v = transform.rotation.eulerAngles;
        v    += new Vector3(0,0, Mathf.Cos(Time.fixedTime) / Mathf.PI / 2);
        transform.rotation = Quaternion.Euler(v);
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