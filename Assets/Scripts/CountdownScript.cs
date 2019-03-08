using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownScript : MonoBehaviour {

	private float startTime;
	private float timePerCount = 2f;
	private int curCount = 2;

	private TextMeshPro tmp;

	void Start () {
		tmp = GetComponent<TextMeshPro>();
		startTime = Time.time;
	}

	void Update () {
		float elapsed = Time.time-startTime;
		if(elapsed>timePerCount && elapsed<2*timePerCount){
			if(curCount>1){
				curCount = 1;
				tmp.SetText("Set!");
			}
		}else if(elapsed>2*timePerCount && elapsed<3*timePerCount){
			if(curCount>0){
				curCount = 0;
				tmp.SetText("Go!");
				transform.root.GetComponent<GameManagerScript>().BeginRace();
			}
		}else if(elapsed>3*timePerCount && elapsed<4*timePerCount){
			Destroy(gameObject);
		}
	}
}
