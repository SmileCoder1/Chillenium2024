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
        if(player.transform.position.y > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, .33f)).y)
        {
            Vector2 v = Vector2.Lerp(transform.position, player.transform.position, .01f);
            if (v.y < transform.position.y) return;
            transform.position = new Vector3(transform.position.x, v.y, transform.position.z);
        }

        if(player.transform.position.y + 1 < GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0,0)).y)
        {
            player.GetComponent<Killable>().Kill();
        }
    }
}
