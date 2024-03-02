using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private AudioSource source;
    public AudioClip wav;
    public float loopPoint;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        startTime = Time.time;
        source.clip = wav;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= loopPoint) { 
            startTime = Time.time;
            source.time = 0;
            source.Play();
        }
    }
}
