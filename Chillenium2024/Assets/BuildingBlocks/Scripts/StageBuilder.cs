using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    public Transform MonkeyObj;
    public GameObject emptyWallInst;
    public GameObject groundInst;
    public GameObject dom;
    public GameObject switchLUL;
    public GameObject sub;
    // Start is called before the first frame update
    void Start()
    {
        switchLUL = FindAnyObjectByType<StageBlock>().gameObject;
        switchLUL.GetComponent<StageBlock>().attachToOld(null);
        dom = Instantiate(emptyWallInst);
        dom.GetComponent<StageBlock>().attachToOld(switchLUL.GetComponent<StageBlock>());
        
    }

    public void loadNext()
    {
        Debug.Log("should switch here");
        if (sub != null)
            Destroy(sub);
        sub = switchLUL;
        switchLUL = dom;
        dom = Instantiate(emptyWallInst);
        dom.GetComponent<StageBlock>().attachToOld(switchLUL.GetComponent<StageBlock>());
    }



}
