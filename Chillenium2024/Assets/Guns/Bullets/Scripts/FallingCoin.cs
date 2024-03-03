using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoin : MonoBehaviour
{
    public float lifeSpan = 1.5f;
    public float age = 0f;

    AudioClip clip;
    AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0.2f;
        
    }

    private void Update()
    {
        age += Time.deltaTime;
        if(lifeSpan <= age)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            source.time = Random.Range(0f, 0.2f);
            source.Play();
        }
    }

}
