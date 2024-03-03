using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool right = false;
    
    public List<float> switchSpots = new List<float>();

    public void addSwitchSpot(float num)
    {
        switchSpots.Add(num);
    }


    public void setCameraDir(float dir)
    {
        
    }

    public void setCameraSize(float size)
    {
        GetComponent<Camera>().orthographicSize = size;
    }

    // Update is called once per frame
    void Update()
    {
        if(switchSpots.Count > 0)
        {
            if(right)
            {
                if( transform.position.x >= switchSpots[0])
                {
                    right = false;
                    transform.position = new Vector3(switchSpots[0], transform.position.y, transform.position.z);
                    switchSpots.RemoveAt(0);
                }
            }
            else
            {
                if(transform.position.y >= switchSpots[0])
                {
                    right = true;
                    transform.position = new Vector3(transform.position.x, switchSpots[0], transform.position.z);
                    switchSpots.RemoveAt(0);
                }
            }
        }
        
        if (!right)
        {
            if (player.transform.position.y > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, .33f)).y)
            {
                Vector2 v = Vector2.Lerp(transform.position, player.transform.position, .01f);
                if (v.y < transform.position.y) return;
                transform.position = new Vector3(transform.position.x, v.y, transform.position.z);
                Debug.Log("Stuff happens");
            }
            Debug.Log("should be going up rn");
            transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y + 0.01f, transform.position.y), transform.position.z);

        }
        else
        {
            if(player.transform.position.x > GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0.33f)).x)
            {
                Vector2 v = Vector2.Lerp(transform.position, player.transform.position, .01f);
                if (v.x < transform.position.x) return;
                transform.position = new Vector3(v.x, transform.position.y, transform.position.z);
            }
            transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.x + Time.deltaTime / 0.4f, transform.position.x), transform.position.z);
        }
        

        if(player.transform.position.y + 1 < GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0,0)).y ||
            player.transform.position.x + 1 < GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).x)
        {
            if (GetComponent<Camera>().orthographicSize > 7)
                player.GetComponent<Killable>().Kill();
            else
                GetComponent<Camera>().orthographicSize += 0.1f;
        }
    }
}
