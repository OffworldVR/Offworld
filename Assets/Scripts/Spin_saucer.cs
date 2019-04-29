using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_saucer : MonoBehaviour {
	float rotateSpeed = -40f;
	void Update () {
			transform.RotateAround(transform.position, transform.up, rotateSpeed*Time.deltaTime);
	}
}
