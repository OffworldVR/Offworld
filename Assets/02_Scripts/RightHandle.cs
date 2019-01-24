using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandle : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider c){
		if(c.transform.root.tag == "Player"){
			c.transform.root.GetComponent<PlayerScript>().HandleTriggered(1, true);
		}
	}

	void OnTriggerExit(Collider c){
		if(c.transform.root.tag == "Player"){
			c.transform.root.GetComponent<PlayerScript>().HandleTriggered(1, false);
		}
	}
}
