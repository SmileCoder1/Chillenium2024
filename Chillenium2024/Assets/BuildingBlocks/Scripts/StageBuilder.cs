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
        switchLUL = Instantiate(groundInst);
        switchLUL.GetComponent<StageBlock>().attachToOld(null);
        dom = Instantiate(emptyWallInst);
        dom.GetComponent<StageBlock>().attachToOld(switchLUL.GetComponent<StageBlock>());
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monkey")
        {
            Debug.Log("should switch here");
            if (sub != null)
                Destroy(sub);
            sub = switchLUL;
            dom = Instantiate(emptyWallInst);
            dom.GetComponent<StageBlock>().attachToOld(switchLUL.GetComponent<StageBlock>());

        }
    }


}
