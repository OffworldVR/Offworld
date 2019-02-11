using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour {

	public int hoopNum;
	public bool forPlayer;

	// Use this for initialization
	void Start () {
		if(!forPlayer){
			transform.GetChild(0).gameObject.SetActive(false);
		}
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
