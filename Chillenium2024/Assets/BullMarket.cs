using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BullMarket : MonoBehaviour
{
    GameObject tri0;
    GameObject tri1;
    public GameObject bull;
    // Start is called before the first frame update
    void Start()
    {
        var tris = gameObject.GetComponentsInChildren<Triangle>();
        tri0 = tris[0].gameObject;
        tri1 = tris[1].gameObject;

        tri0.transform.Translate( new Vector3(-100, 0, 0));
        tri1.transform.Translate( new Vector3(100, 0, 0));
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
        for(int i = 0; i < 150; i++) {
            tri0.transform.Translate( new Vector3(100f/150f, 0, 0));
            tri1.transform.Translate(new Vector3(-100f/150f, 0, 0));
            yield return new WaitForSecondsRealtime(.001f);
        }

        for(int i = 0; i < 150; i++)
        {
            var b = Instantiate(bull);
            var size = Camera.main.orthographicSize * 2f;
            var width = size * (float)Screen.width / Screen.height;
            var height = size;
            var side = Random.Range(-width - 1, width + 1);
            var pos = new Vector3(side + Camera.main.transform.position.x, Random.Range(-height, height) + Camera.main.transform.position.y);
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
        for (int i = 0; i < 150; i++)
        {
            tri0.transform.Translate(new Vector3(-100f / 150f, 0, 0));
            tri1.transform.Translate(new Vector3(100f / 150f, 0, 0));
            yield return new WaitForSecondsRealtime(.001f);
        }

    }

    IEnumerator B(GameObject b, int dir)
    {
        b.gameObject.SetActive(true);
        for(int i =0; i < 300; i++)
        {
            b.transform.position += new Vector3(dir/6f, 0, 0);
            yield return new WaitForSecondsRealtime(.01f);
        }
        Destroy(b.gameObject);
    }
}
