using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    [SerializeField] GameObject monkey;
    [SerializeField] GameObject gun;
    //[SerializeField] float damping;
    //[SerializeField] float rotAmount;
    //bool hingeFlip = false;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseScreenPosition.z = 0;

        Vector3 monkeyDirection = (mouseScreenPosition - monkey.transform.position).normalized;
        //monkey.transform.right = Vector3.Lerp(monkey.transform.right, monkeyDirection, Time.deltaTime * damping);
        monkey.transform.right = monkeyDirection;

        Vector3 gunDirection = (mouseScreenPosition - monkey.transform.position).normalized;
        //gun.transform.right = Vector3.Lerp(gun.transform.right, gunDirection, Time.deltaTime * damping);
        gun.transform.right = gunDirection;

        //if(monkey.transform.rotation.eulerAngles.z > 180 && !hingeFlip)
        //{
        //    foreach (var hinge in monkey.GetComponentsInChildren<HingeJoint2D>())
        //    {
        //        JointAngleLimits2D limit = hinge.limits;
        //        float min_angle = hinge.limits.min;
        //        float max_angle = hinge.limits.max;
        //        limit.min = max_angle;
        //        limit.max = min_angle;
        //        hinge.limits = limit;
        //    }
        //    hingeFlip = true;
        //    Debug.Log(monkey.transform.rotation.z);
        //}
        //else if (monkey.transform.rotation.eulerAngles.z < 180 && hingeFlip)
        //{
        //    foreach (var hinge in monkey.GetComponentsInChildren<HingeJoint2D>())
        //    {
        //        JointAngleLimits2D limit = hinge.limits;
        //        float min_angle = hinge.limits.min;
        //        float max_angle = hinge.limits.max;
        //        limit.min = max_angle;
        //        limit.max = min_angle;
        //        hinge.limits = limit;
        //    }
        //    hingeFlip = false;
        //    Debug.Log(monkey.transform.rotation.z);
        //}
    }
}
