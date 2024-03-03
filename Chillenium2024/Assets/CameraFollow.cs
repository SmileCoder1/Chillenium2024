using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public List<float> switchSpots = new List<float>();
    public bool rightDir = false;


    public void setDirection(float switchLoc)
    {
        switchSpots.Add(switchLoc);
    }


    // Update is called once per frame
    void Update()
    {
        if(switchSpots.Count > 0)
        {
            if (rightDir)
            {
                if (transform.position.x >= switchSpots[0])
                {
                    rightDir = false;
                    switchSpots.RemoveAt(0);
                }
            }
            else
            {
                if(transform.position.y >= switchSpots[0])
                {
                    rightDir = true;
                    switchSpots.RemoveAt(0);
                }
            }
            
        }


        if (!rightDir)
        {


            if (player.transform.position.y > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, .33f)).y)
            {
                Vector2 v = Vector2.Lerp(transform.position, player.transform.position + Vector3.up * 2f, .01f);
                if (v.y < transform.position.y) return;
                transform.position = new Vector3(transform.position.x, v.y, transform.position.z);
            }

            if (player.transform.position.y + 2 < GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).y)
            {
                player.GetComponent<Killable>().Kill();
            }
        }
        else
        {
            if (player.transform.position.x > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, .33f)).x)
            {
                Vector2 v = Vector2.Lerp(transform.position, player.transform.position + Vector3.right * 2f, .01f);
                if (v.x < transform.position.x) return;
                transform.position = new Vector3(v.x, transform.position.y, transform.position.z);
            }

            if (player.transform.position.x + 2 < GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).x)
            {
                player.GetComponent<Killable>().Kill();
            }
        }
    }
}
