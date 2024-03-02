using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBehavior : ScriptableObject
{
    public float cooldown;
    public float force;

    protected GunBehavior() { }
}