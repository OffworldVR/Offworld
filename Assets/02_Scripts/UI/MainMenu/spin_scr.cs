using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin_scr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0f, 0f, 1f));
	}
}
