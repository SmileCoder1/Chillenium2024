using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialOnStartup : MonoBehaviour
{
    
    
    public GameObject tmpro1 = null;
    public GameObject tmpro2 = null;
    public int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        tmpro1.SetActive(true);
        Time.timeScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        bool toCont = Input.GetKeyDown(KeyCode.Space);

        if(toCont && i == 0)
        {
            i++;
            tmpro1.SetActive(false);
            tmpro2.SetActive(true);
        }
        else if (toCont && i == 1)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}
