using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image bar1;
    public Image bar2;
    public Image bar3;
    public Image bar4;
    public Image bar5;
    
    // Start is called before the first frame update
    void Start()
    {
        bar1.enabled = false;
        bar2.enabled = false;
        bar3.enabled = false;
        bar4.enabled = false;
        bar5.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            bar1.enabled = true;
        }
        if (Input.GetKeyDown("2"))
        {
            bar2.enabled = true;
        }
        if (Input.GetKeyDown("3"))
        {
            bar3.enabled = true;
        }
        if (Input.GetKeyDown("4"))
        {
            bar4.enabled = true;
        }
        if (Input.GetKeyDown("5"))
        {
            bar5.enabled = true;
        }

    }
}
