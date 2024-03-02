using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Aiming : MonoBehaviour
{
    public GameObject Gun;
    public Rigidbody2D Rb;


    public int hfScreenWidth;
    public int hfScreenHeight;
    private float radToDegree = 57.2957795131f;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        hfScreenHeight = Screen.height / 2;
        hfScreenWidth = Screen.width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 linebw = mousePos - transform.position;
        float angle = Mathf.Atan2(linebw.x, linebw.y) * radToDegree;
        Gun.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
