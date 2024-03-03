using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour
{
    // Start is called before the first frame update

    private Image im;
    void Start()
    {
        im = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var c = im.color;
        c.a = (Mathf.Cos(Time.realtimeSinceStartup / 1.5f) + 1f);
        im.color = c;
    }
}
