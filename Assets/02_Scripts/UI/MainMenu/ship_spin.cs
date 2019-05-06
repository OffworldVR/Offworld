using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_spin : MonoBehaviour {

    float tick;
    float period;
	// Use this for initialization
	void Start () {
        tick = 0f;
        period = (2.0f * Mathf.PI) / 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).Rotate(0, -1 * Mathf.Sin(period * tick), 0);
        tick += Time.deltaTime;
	}
}
