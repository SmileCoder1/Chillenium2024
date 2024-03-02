using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, .75f)).y)
        {
            Vector2 v = Vector2.Lerp(transform.position, player.transform.position, .01f);
            transform.position = new Vector3(transform.position.x, v.y, transform.position.z);
        }
    }
}