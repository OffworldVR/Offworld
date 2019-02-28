using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasers : items {

    private GameObject laser1;
    private GameObject laser2;
    // Use this for initialization
    void Start ()
    {
        laser1 = transform.Find("Laser1").gameObject;
        laser2 = transform.Find("Laser2").gameObject;
    }
	

    public void activateItem()
    {
        if (PlayerScript.leftTriggerIsTriggered)
        {
            laser1.SetActive(true);
            laser2.SetActive(true);
        }
        else
        {
            laser1.SetActive(false);
            laser2.SetActive(false);
        }
    }
}
