using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : GunBehavior
{
    public Pistol()
    {
         cooldown = 0.5f;
         force = 10;
    }
}
