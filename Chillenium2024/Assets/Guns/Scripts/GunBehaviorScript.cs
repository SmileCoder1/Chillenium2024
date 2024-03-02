using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviorScript : MonoBehaviour
{

    public float cooldown = 0.5f;
    public float timeSinceShot = 0;
    public float force = 10;
    public bool mousePressed = false;
    public Bullet bulletType;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(timeSinceShot >= cooldown)
        {
            timeSinceShot = 0;
            Bullet bullet = Instantiate(bulletType);
            bullet.shoot(transform.position, gameObject.transform.eulerAngles.z + 90); //idk why we need the 90 but it doesn't work without it
            transform.parent.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * -1 * bulletType.recoil);
        }

    }
    
}
