using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	bool leftHandleIsTriggered = false;
	bool rightHandleIsTriggered = false;
	bool leftTriggerIsTriggered = false;
	bool rightTriggerIsTriggered = false;
	bool canSteer = false;

	void Start () {

	}

	void Update () {

	}

	public void HandleTriggered(int handleNum, bool isTriggered){
		if(handleNum==0){
			leftHandleIsTriggered = isTriggered;
		}else{
			rightHandleIsTriggered = isTriggered;
		}
		CheckSteering();
	}
	public void TriggerTriggered(int handNum, bool isTriggered){
		if(handNum==0){
			leftTriggerIsTriggered = isTriggered;
		}else{
			rightTriggerIsTriggered = isTriggered;
		}
		CheckSteering();
	}

	public void CheckSteering(){
		canSteer = leftHandleIsTriggered && rightHandleIsTriggered && leftTriggerIsTriggered && rightTriggerIsTriggered;
		Debug.Log(canSteer);
	}
}
