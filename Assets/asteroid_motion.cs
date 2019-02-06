using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid_motion : MonoBehaviour {
	Vector3 dir;
	public float rot_force = 1000f;
	// Use this for initialization
	void Start () {
		dir = new Vector3(Random.Range(0,10),Random.Range(0,10),Random.Range(0,10));
	}

	void FixedUpdate() {
		float h = rot_force * Time.deltaTime;
    float v = rot_force * Time.deltaTime;

		gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * h);
    gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * v);
		gameObject.GetComponent<Rigidbody>().AddForce(dir * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {

	}
}
