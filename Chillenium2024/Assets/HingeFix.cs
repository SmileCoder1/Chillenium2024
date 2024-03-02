using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeFix : MonoBehaviour
{
    [SerializeField] float minAngle;
    [SerializeField] float maxAngle;

    private HingeJoint2D hinge;
    private bool hingeOff;
    bool hingeFlip = false;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.y = Mathf.Clamp(rot.y, minAngle, maxAngle);
        transform.localEulerAngles = rot;

        //if (transform.localEulerAngles.z < 0 && !hingeOff && !hingeFlip)
        //{
        //    hinge.useLimits = false;
        //    hingeOff = true;
        //    hingeFlip = true;
        //}
        //else if (transform.localEulerAngles.z > 0 && !hingeOff && hingeFlip)
        //{
        //    hinge.useLimits = false;
        //    hingeOff = true;
        //    hingeFlip = false;
        //}

        //if (hingeOff)
        //{
        //    hinge.useLimits = true;
        //    hingeOff = false;
        //}
    }
}
