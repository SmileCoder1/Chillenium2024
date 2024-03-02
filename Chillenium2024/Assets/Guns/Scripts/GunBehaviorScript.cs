using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GunDef
{
    public string name;
    public float cooldown;
    public float force;
    public float maxBullets;
    public Sprite sprite;
}

public class GunBehaviorScript : MonoBehaviour
{
    public bool mousePressed = false;
    public Bullet bulletType;
    public GameObject smokeEffect;
    public GameObject sparkEffect;
    public float timeSinceShot;
    public float bulletsLeft;


    [Serializable]
    public enum GunType
    {
        Default,
            AK,
            MiniGun
    }

    
    public Dictionary<GunType, GunDef> gunDefs;

    [System.Serializable]
    public struct SKVP
    {
        public GunType type;
        public GunDef def;
    }

    [SerializeField]
    private SKVP[] gunDefsInit;

    private GunType gun;

    // Start is called before the first frame update
    void Awake()
    {
        gunDefs = new Dictionary<GunType, GunDef>();
        foreach(var def in gunDefsInit)
        {
            gunDefs.Add(def.type, def.def);
        }
        SwitchTo(GunType.Default);
    }

    private void Update()
    {
        if(bulletsLeft < 1)
        {
            SwitchTo(GunType.Default);
        }
        if (Input.GetMouseButton(0))
            shootReq();
    }

    private void FixedUpdate()
    {
        timeSinceShot += Time.fixedDeltaTime;
      
        
    }

    public void SwitchTo(GunType type)
    {
        gun = type;
        transform.parent.GetComponent<SpriteRenderer>().sprite = gunDefs[type].sprite;
        bulletsLeft = gunDefs[type].maxBullets;
    }


    void shootReq()
    {
        if(timeSinceShot >= gunDefs[gun].cooldown)
        {
            bulletsLeft--;
            timeSinceShot = 0;
            Bullet bullet = Instantiate(bulletType, transform.position + transform.forward, transform.rotation);
            Instantiate(smokeEffect, transform.position, transform.rotation * new Quaternion(0, 0, 0, 1));
            Instantiate(sparkEffect, transform);
            bullet.shoot(transform.position + 1 * gameObject.transform.eulerAngles.normalized, gameObject.transform.eulerAngles.z + 90); //idk why we need the 90 but it doesn't work without it
            transform.parent.parent.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * -1 * bulletType.recoil);
        }

    }
    
}
