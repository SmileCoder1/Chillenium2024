using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviorScript : MonoBehaviour
{

    public GunBehavior GunBehavior;
    public bool mousePressed = false;
    public Bullet bulletType;
    public GameObject smokeEffect;
    public GameObject sparkEffect;
    public float timeSinceShot;
    public GameObject firePoint;

    // Start is called before the first frame update
    void Start()
    {
        GunBehavior = new Pistol();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            shootReq();
    }

    private void FixedUpdate()
    {
        timeSinceShot += Time.fixedDeltaTime;
        
    }


    void shootReq()
    {
        if(timeSinceShot >= GunBehavior.cooldown)
        {
            timeSinceShot = 0;
            Bullet bullet = Instantiate(bulletType, transform.position, transform.rotation);
            Instantiate(smokeEffect, transform.position, transform.rotation * new Quaternion(0, 0, 0, 1));
            Instantiate(sparkEffect, transform);
            Debug.Log(firePoint.transform.position);
            bullet.shoot(firePoint.transform.position, gameObject.transform.eulerAngles.z + 90); //idk why we need the 90 but it doesn't work without it
            transform.parent.parent.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * -1 * bulletType.recoil);
        }

    }
    
}
