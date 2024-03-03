using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public Rope parent;
    private AudioSource hitSource;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.position.x > GameObject.FindWithTag("Player").transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void sfx()
    {
        hitSource = gameObject.AddComponent<AudioSource>();
        hitSource.clip = sound;
        hitSource.volume = 0.25f;

        hitSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endEverything()
    {
        parent.DIE();
        Destroy(this.gameObject);
    }
}
