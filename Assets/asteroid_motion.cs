using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid_motion : MonoBehaviour {
	Vector3 dir;
	public float motion_vel = 100000000f;
	public float rot_force = 1000f;
	public float time_till_turn = 40f;
	float timer;
	bool enabled = false;
	// Use this for initialization
	void Start () {
		dir = Vector3.Normalize(new Vector3(Random.Range(0,10),Random.Range(0,10),Random.Range(0,10)));
		timer = time_till_turn;
	}
	void FixedUpdate() {
		transform.position += (dir * motion_vel * Time.deltaTime);
		float h = rot_force * Time.deltaTime;
    float v = rot_force * Time.deltaTime;

		gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * h);
    gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * v);
		if(timer >= 0.0) {
			timer -= Time.fixedDeltaTime;
		} else {
			timer = time_till_turn;
			dir = -1 * dir;
		}
	}
}
