using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class UsefulConsts
{
    public static float degreeToRad = 0.0174532925199f;
    public static float radToDegree = 57.2957795131f;
    public static Vector2 unitVecFromAngle(float deg)
    {
        return new Vector2(Mathf.Cos(deg * UsefulConsts.degreeToRad), Mathf.Sin(deg * UsefulConsts.degreeToRad));
    }
}


public class Bullet : MonoBehaviour
{

    public float maxDist = 10f;
    public List<string> bounceableTags = new List<string>(new[] { "Wall" });
    public List<string> termTags = new List<string>(new[] { "Enemy" });
    public float recoil = 30f;

    public virtual void preComp()
    {
        Debug.Log("preComp Not Defined");
        return;
    }


    public virtual void handleGunShot(Vector2 start, float dir)
    {
        Debug.Log("handleGunShot Not Defined");
        return;

    }

    public virtual void renderGunShot()
    {
        Debug.Log("Render Not Defined");
        return;
    }

    public virtual void shoot(Vector2 start, float dir)
    {
        handleGunShot(start, dir);
    }

    

    

    

}
