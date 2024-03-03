using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Killable : MonoBehaviour
{
    
    public IEnumerator Kill()
    {
        HingeJoint2D[] monkeyLimbs = FindObjectsOfType<HingeJoint2D>();
        foreach(HingeJoint2D limb in monkeyLimbs)
        {
            limb.enabled = false;
            Rigidbody2D obj = limb.gameObject.GetComponent<Rigidbody2D>();
            if(obj != null)
            {
                obj.AddForce(new Vector2(Random.Range(-100, 100), Random.Range(-100, 100)));
            }
        }
        yield return new WaitForSeconds(2f);
        //Debug.LogError("I DIED");
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }
}
