using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour {

	public int hoopNum;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag=="Player"){
			float d = (c.transform.position-transform.position).magnitude;
			if(d<40){
				if(d<25){
					Debug.Log("hit inner");
				}else{
					Debug.Log("hit outer");
				}
			}
		}
	}
}
