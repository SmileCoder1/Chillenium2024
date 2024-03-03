using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class score : MonoBehaviour
{
    private float s = 0;
    private float rs = 0;
    public TMPro.TMP_Text scoreText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        var lrs = s + (int)(transform.position.y / 10f);
        rs = lrs > rs ? lrs : rs;
        scoreText.text = "Score: " + rs;
    }

    public void KilledEnemy()
    {
        s++;
    }
}
