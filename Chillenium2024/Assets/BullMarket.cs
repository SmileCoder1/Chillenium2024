using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BullMarket : MonoBehaviour
{
    GameObject tri0;
    GameObject tri1;
    public GameObject bull;
    public AudioClip clip;
    public AudioClip train;
    private AudioSource s;
    // Start is called before the first frame update
    void Start()
    {
        var tris = gameObject.GetComponentsInChildren<Triangle>();
        tri0 = tris[0].gameObject;
        tri1 = tris[1].gameObject;

        tri0.transform.position +=  new Vector3(-30, 0, 0);
        tri1.transform.position += new Vector3(30, 0, 0);
        s = gameObject.AddComponent<AudioSource>();
        s.clip = train;
    }

    public void BULL_MARKET()
    {
        foreach(var enemy in FindObjectsOfType<Bug>())
        {
            Destroy(enemy.gameObject);
        }
        StartCoroutine(BM());
    }

    IEnumerator BM()
    {
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        s.Play();
        for(int i = 0; i < 50; i++) {
            tri0.transform.position += new Vector3(30f / 50f, 0, 0);
            tri1.transform.position += new Vector3(-30f / 50f, 0, 0);
            yield return new WaitForSecondsRealtime(.001f);
        }

        for(int i = 0; i < 150; i++)
        {
            var b = Instantiate(bull);
            var size = Camera.main.orthographicSize * 2f;
            var width = size * (float)Screen.width / Screen.height;
            var height = size;
            var side = UnityEngine.Random.Range(-width - 1, width + 1);
            var pos = new Vector3(side + Camera.main.transform.position.x, UnityEngine.Random.Range(-height, height) + Camera.main.transform.position.y);
            b.transform.parent = transform.parent;
            b.transform.position = pos;
            if(side < 0)
            {
                b.GetComponent<SpriteRenderer>().flipX = true;
            }
            StartCoroutine(B(b, side < 0 ? 1 : -1));
        }

        yield return new WaitForSecondsRealtime(3f);
        foreach (var enemy in FindObjectsOfType<Bug>())
        {
            Destroy(enemy.gameObject);
        }
        for (int i = 0; i < 50; i++)
        {
            tri0.transform.position += new Vector3(-30f / 50f, 0, 0);
            tri1.transform.position += new Vector3(30f / 50f, 0, 0);
            yield return new WaitForSecondsRealtime(.001f);
        }
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }

    IEnumerator B(GameObject b, int dir)
    {
        var rot = UnityEngine.Random.Range(-.01f, .01f);
        b.gameObject.SetActive(true);
        var s = b.gameObject.AddComponent<AudioSource>();
        s.clip = clip;
        if (0 == UnityEngine.Random.Range(0, 5))
        StartCoroutine(RandDelay(s.Play));
        for (int i =0; i < 200; i++)
        {
            b.transform.position += new Vector3(dir/6f, 0, 0);
            b.transform.RotateAround(b.transform.position, b.transform.forward, rot);
            yield return new WaitForSecondsRealtime(.01f);
        }
        Destroy(b.gameObject);
    }

    IEnumerator RandDelay(Action a)
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(0f, 2f));
        a();
    }
}
