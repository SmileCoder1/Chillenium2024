using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float startTime;
    public float maxDifficultyTime;
    public float minDifficultyRate;
    public float maxDifficultyRate;
    private float lastSpawn;
    [System.Serializable]
    public struct spawnStruct
    {
        public GameObject obj;
        public float rate;
    }
    [SerializeField]
    public List<spawnStruct> spawns;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastSpawn = Time.time;
    }


    float difficultyFunction() //returns rate of spawn in enemies per second
    {
        return (Time.time - startTime)/maxDifficultyTime * (minDifficultyRate + (maxDifficultyRate - minDifficultyRate));
    }
    // Update is called once per frame
    void Update()
    {
        
        if(Time.time - lastSpawn > 1 / difficultyFunction())
        {
            lastSpawn = Time.time;
            float runningTotal = 0;
            float randVal = Random.Range(0f, 1f);
            foreach(spawnStruct spawn in spawns)
            {
                runningTotal += spawn.rate;
                if (randVal <= runningTotal)
                {
                    Instantiate(spawn.obj);
                }
            }
        }

    }
}