﻿using System.Collections;
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

    private Vector3 saveGrabPos;
    private Vector3 OriginalGrabPosition;
    private Vector3 NewGrabPosition;
    private Vector3 wheelStartPosition;

	private Rigidbody rb;

	private float wheelRotation;
	private float wheelDistance;

    public shipMovement shipScript;

    //Determines for of acceleration
    float thrust = 0.5f;

    //Changed by LH_Listener to 0 if force is positive or 1 if negative 
    public int forwardOrBack = 0;


    void Start () {
        rb = GetComponent<Rigidbody>();

        //Set original wheel Position
        wheelStartPosition = wheel.localPosition;

        //Get ship script
        shipScript = GameObject.Find("Ship").GetComponent<shipMovement>();
    }

    void Update () {
        if (canSteer)
        {
            NewGrabPosition = rightHandTransform.position;

            UpdatePitch();
            UpdateRoll();
            Accelerate();


        }
    }

    public void UpdatePitch()
    {
        //Move wheel z position to hand z position
        wheelDistance = Mathf.Clamp(rightHandTransform.localPosition.z, 0, 0.6f);
        wheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, wheelDistance);

        float deltaDistance = wheelStartPosition.z - wheelDistance;
        deltaDistance = deltaDistance * -100f;
        transform.RotateAround(transform.position, transform.right, deltaDistance * Time.deltaTime);

    }
    public void UpdateRoll()
    {

        //Calculate angle between a x-axis constant vector and the vector from the center of the steering wheel to the current hand position
		Vector3 vector1 = transform.right;
        Vector3 vector2 = new Vector3(NewGrabPosition.x, NewGrabPosition.y, 0) - new Vector3(wheel.position.x, wheel.position.y, 0);

        //Calculate difference between the two vectors
        float degreeBetween = Vector3.Angle(vector1, vector2);

        //Calculate cross product to determine polarity of angle
        Vector3 cross = Vector3.Cross(vector1, vector2);
        if (cross.z < 0) degreeBetween = -degreeBetween;

        //Rotate wheel GameObject
        wheel.eulerAngles = new Vector3(0, 0, degreeBetween);

        //RollShip 
        transform.RotateAround(transform.position, transform.forward, degreeBetween * Time.deltaTime);

    }
  
	public void HandleTriggered(int handleNum, bool isTriggered){
		if(handleNum==0){
						leftHandleIsTriggered = isTriggered;
		}
        else {
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
				wheelStartPosition = wheel.position;
    }
		canSteer = rightHandleIsTriggered && rightTriggerIsTriggered;
            //wheelStartPos =
	}

    public void Accelerate()
    {

        Vector3 force = new Vector3(0, 0, 0);

        if (forwardOrBack == 1)
        {
            force = transform.forward * thrust;
        }
        else if (forwardOrBack == -1)
        {

            force = -transform.forward * thrust;

        }

        //Add Force
        rb.AddForce(force);


    }
}
