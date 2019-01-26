using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	bool leftHandleIsTriggered = false;
	bool rightHandleIsTriggered = false;
	bool leftTriggerIsTriggered = false;
	bool rightTriggerIsTriggered = false;
	bool canSteer = false;

    public Transform rightHandTransform;
    public Transform leftHandTransform;
    public Transform wheel;

    private Vector3 OriginalGrabPosition;
    private Vector3 NewGrabPosition;
    private Vector3 wheelStartPos;



	void Start () {
        wheelStartPos = wheel.localPosition;
	}

	void Update () {



        if (canSteer)
        {
            NewGrabPosition = rightHandTransform.localPosition;

            UpdateWheel();
            UpdatePitch();
            UpdateRoll();
        }
	}
    public void UpdatePitch()
    {

        //float distanceBetween = OriginalGrabPosition.z - NewGrabPosition.z;

        //wheel.localPosition = wheelStartPos + new Vector3(0,0, -distanceBetween);
        wheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, rightHandTransform.localPosition.z);
       // Debug.Log(distanceBetween);

    }
    public void UpdateRoll()
    {
        Vector3 vector1 = OriginalGrabPosition - wheel.localPosition;
        Vector3 vector2 = NewGrabPosition - wheel.localPosition;

        float degreeBetween= Vector3.Angle(vector1, vector2);
        Vector3 cross = Vector3.Cross(vector1, vector2);
        if (cross.y < 0) degreeBetween = -degreeBetween;

        wheel.eulerAngles = new Vector3(0, 0, degreeBetween);
    }
    public void UpdateWheel()
    {

    }

	public void HandleTriggered(int handleNum, bool isTriggered){
		if(handleNum==0)
        {
			leftHandleIsTriggered = isTriggered;
		}
        else
        {
			rightHandleIsTriggered = isTriggered;
            //Set Position where wheel is grabbed
          

        }
        CheckSteering();
	}
	public void TriggerTriggered(int handNum, bool isTriggered){

        //Run if left hand grip is grabbed or released
        if (handNum == 0)
        {
            leftTriggerIsTriggered = isTriggered;
        }
        //Run if right hand grip is grabbed or released
        else
        {
            rightTriggerIsTriggered = isTriggered;
        }
		CheckSteering();
	}

	public void CheckSteering(){
        //canSteer = leftHandleIsTriggered && rightHandleIsTriggered && leftTriggerIsTriggered && rightTriggerIsTriggered;
        if (!canSteer)
        {
            canSteer = rightHandleIsTriggered && rightTriggerIsTriggered;
            OriginalGrabPosition = rightHandTransform.localPosition;
        }
        else
        {
            canSteer = rightHandleIsTriggered && rightTriggerIsTriggered;
        }
            //wheelStartPos = 
	}

    
}
