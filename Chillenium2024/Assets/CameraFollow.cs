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
    public bool justFollowMode = false;


    public void setDirection(float switchLoc)
    {
        switchSpots.Add(switchLoc);
    }


    // Update is called once per frame
    void Update()
    {
        



        if(justFollowMode)
        {
            Vector3 t = player.transform.position;
            GetComponent<Camera>().transform.position = new Vector3(t.x, t.y, -10);
            GetComponent<Camera>().orthographicSize = 3;
            return;
        }


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

        if (player != null)
        {
            if (player.transform.position.y + 2 < GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).y)
            {
                StartCoroutine(player.GetComponent<Killable>().Kill());
            }
            if (player.transform.position.x + 2 < GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).x)
            {
                StartCoroutine(player.GetComponent<Killable>().Kill());
            }
        }

        if (!rightDir)
        {


            if (player.transform.position.y > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0.45f)).y)
            {
                float coeff= 2 * GetComponent<Camera>().WorldToViewportPoint(player.transform.position).y / 0.45f;
                Vector2 v = Vector2.Lerp(transform.position, player.transform.position + Vector3.up * 2f, coeff * 0.01f);
                if (v.y < transform.position.y) return;
                transform.position = new Vector3(transform.position.x, v.y, transform.position.z);
            }
            
            
        }
        else
        {
            if (player.transform.position.x > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, .45f)).x)
            {
                float coeff = 2 * GetComponent<Camera>().WorldToViewportPoint(player.transform.position).x / 0.45f;
                Vector2 v = Vector2.Lerp(transform.position, player.transform.position + Vector3.right * 2f, coeff * .01f);
                if (v.x < transform.position.x) return;
                transform.position = new Vector3(v.x, transform.position.y, transform.position.z);
            }
        }
    }
}
