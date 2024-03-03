using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<float> probabilities = new List<float>();
    public List<Transform> spawnLocs = new List<Transform>();
    public List<int> topBot = new List<int>();
    public float probability = 0.25f;
    public int side = 0;
    public float timeSince = 0f;
    public float spawnTimer = 1f;
    

    float getProbability()
    {
        float rooms = FindAnyObjectByType<GameManager>().getRoomCount();
        return 1 - Mathf.Exp(-0.4f * rooms);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeSince = Random.Range(0f, 1f);
        spawnTimer = 2 - getProbability();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSince < spawnTimer)
        {
            timeSince += Time.deltaTime;
            return;
        }


        if(Random.Range(0f, 1f) < probability)
        {
            bool spawned = false;
            for (int i = 0; i < enemies.Count - 1; i++)
            {
                if (probabilities[i] > Random.Range(0f, 1f))
                {
                    spawned = true;
                    GameObject obj = Instantiate(enemies[i], spawnLocs[i].position, Quaternion.identity);
                    obj.GetComponent<Bug>().side = side;
                    obj.GetComponent<Bug>().dir = topBot[enemies.Count - 1];
                }
            }
            if(!spawned && enemies.Count > 0)
            {
                GameObject obj = Instantiate(enemies[enemies.Count - 1], spawnLocs[enemies.Count - 1].position, Quaternion.identity);
                obj.GetComponent<Bug>().side = side;
                obj.GetComponent<Bug>().dir = topBot[enemies.Count - 1];
            }
        }
        timeSince = 0;

    }
}
