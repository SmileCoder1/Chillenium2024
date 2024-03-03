using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindAnyObjectByType<CameraFollow>().enabled = false;
            GameObject camera = GameObject.Find("Main Camera");
            camera.GetComponent<CameraFollow>().justFollowMode = true;
            StartCoroutine(collision.gameObject.GetComponent<Killable>().Kill());
        }
    }
}