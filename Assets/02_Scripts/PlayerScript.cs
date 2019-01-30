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
		private Rigidbody rb;
		private float wheelRotation;
		private float wheelDistance;

    // public GameObject shipToMove;
    // public shipMovement shipScript;


    private Vector3 saveGrabPos;

    void Start () {
        //Set original wheel Position
        wheelStartPos = wheel.position;
				rb = GetComponent<Rigidbody>();
        //Get ship script
        // shipScript = GameObject.Find("Ship").GetComponent<shipMovement>();
    }

    void Update () {
        if (canSteer)
        {
            NewGrabPosition = rightHandTransform.position;
            UpdateWheel();
            UpdatePitch();
            UpdateRoll();
						Move();
						//TODO REMOVE THIS
						// rb.AddForce(transform.forward);
        }
	}

	public void Move(){
		// transform.eulerAngles = ;
	}
    public void UpdatePitch()
    {
        //float distanceBetween = OriginalGrabPosition.z - NewGrabPosition.z;

        //wheel.localPosition = wheelStartPos + new Vector3(0,0, -distanceBetween);
        wheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, rightHandTransform.localPosition.z);
				wheelDistance = rightHandTransform.localPosition.z;
        // Debug.Log(distanceBetween);

        //Call script to move ship forward
        // shipScript.shipForward(wheelStartPos.z-rightHandTransform.localPosition.z);



    }
    public void UpdateRoll()
    {

        //Calculate angle between 4 points
        // Vector3 vector1 = new Vector3(OriginalGrabPosition.x, OriginalGrabPosition.y, 0) - new Vector3(wheelStartPos.x, wheelStartPos.y, 0);
				Vector3 vector1 = new Vector3(1, 0, 0);
        Vector3 vector2 = new Vector3(NewGrabPosition.x, NewGrabPosition.y, 0) - new Vector3(wheel.position.x, wheel.position.y, 0);

        // saveGrabPos = rightHandTransform.position;



        float degreeBetween = Vector3.Angle(vector1, vector2);

        //Calculate cross product to determine polarity of angle
        Vector3 cross = Vector3.Cross(vector1, vector2);
        if (cross.z < 0) degreeBetween = -degreeBetween;
				wheelRotation = degreeBetween;

        //Rotate physical wheel
        wheel.eulerAngles = new Vector3(0, 0, degreeBetween);



    }
    public void UpdateWheel()
    {

    }

	public void HandleTriggered(int handleNum, bool isTriggered){
		if(handleNum==0){
						leftHandleIsTriggered = isTriggered;
		}else{
				rightHandleIsTriggered = isTriggered;
        //Set Position where wheel is grabbed
    }
    CheckSteering();
	}
	public void TriggerTriggered(int handNum, bool isTriggered){
    //Run if left hand grip is grabbed or released
    if (handNum == 0){
        leftTriggerIsTriggered = isTriggered;
    }else{
    //Run if right hand grip is grabbed or release
        rightTriggerIsTriggered = isTriggered;
    }
		CheckSteering();
	}

	public void CheckSteering(){
        //canSteer = leftHandleIsTriggered && rightHandleIsTriggered && leftTriggerIsTriggered && rightTriggerIsTriggered;
    if (!canSteer){
        OriginalGrabPosition = rightHandTransform.position;
				wheelStartPos = wheel.position;
    }
		canSteer = rightHandleIsTriggered && rightTriggerIsTriggered;
            //wheelStartPos =
	}


}
