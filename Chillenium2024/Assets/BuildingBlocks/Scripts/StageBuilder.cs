using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    public Transform MonkeyObj;
    public GameObject emptyWallInst;
    public GameObject puzzleRoomInst;
    public GameObject groundInst;
    public GameObject dom;
    public GameObject switchLUL;
    public GameObject sub;

    // Start is called before the first frame update
    void Start()
    {
        switchLUL = FindAnyObjectByType<StageBlock>().gameObject;
        switchLUL.GetComponent<StageBlock>().attachToOld(null);
        if (.2 < Random.Range(0f, 1f))
        {
            dom = Instantiate(emptyWallInst);
        }
        else
        {
            dom = Instantiate(puzzleRoomInst);
        }
        dom.GetComponent<StageBlock>().attachToOld(switchLUL.GetComponent<StageBlock>());
        
    }

    public void loadNext()
    {
        Debug.Log("should switch here");
        if (sub != null)
            Destroy(sub);
        sub = switchLUL;
        switchLUL = dom;
        if (.2 < Random.Range(0f, 1f))
        {
            dom = Instantiate(emptyWallInst);
        }
        else
        {
            dom = Instantiate(puzzleRoomInst);
        }
        dom.GetComponent<StageBlock>().attachToOld(switchLUL.GetComponent<StageBlock>());
    }



}
