using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    List<float> probabilities = new List<float>();
    public float probability = 0.25f;
    

    float getProbability()
    {
        float rooms = FindAnyObjectByType<GameManager>().getRoomCount();
        return 1 - Mathf.Exp(-0.4f * rooms);
    }
    
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0f, 1f) < probability)
        {
            bool spawned = false;
            for (int i = 0; i < enemies.Count - 1; i++)
            {
                if (probabilities[i] > Random.Range(0f, 1f))
                {
                    spawned = true;
                    Instantiate(enemies[i]);
                }
            }
            if(!spawned)
            {
                Instantiate(enemies[enemies.Count - 1]);
            }
        }
    }
}
