using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasers : itemPrefabSpawnController
{

    public GameObject laser1;
    public GameObject laser2;

 
    // Use this for initialization
    void Start ()
    {
        //laser1 = transform.Find("Laser1").gameObject;
        //laser2 = transform.Find("Laser2").gameObject;
    }
	

    public void activateItem()
    {
        Debug.Log("Laser Activated");
        if (PlayerScript.leftTriggerIsTriggered)
        {
            Debug.Log("Laser Fired");
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
