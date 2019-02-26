using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    //If itemBlock trigger is entered, call itemSelector Script in the PlayerScript
    void OnTriggerEnter(Collider c)
    {

        if (c.transform.tag == "Player")
        {
            //Make block invisible
						Debug.Log("ITEM BLOCK ENTERED");
            gameObject.GetComponent<Renderer>().enabled = false;

            //Call itemSelector
            c.transform.GetComponent<itemsScript>().ItemSelector();
        }


    }

}
