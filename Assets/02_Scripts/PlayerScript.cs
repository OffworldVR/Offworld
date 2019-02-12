using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerScript : MonoBehaviour {
	bool leftHandleIsTriggered = false;
	bool rightHandleIsTriggered = false;
	bool leftTriggerIsTriggered = false;
	bool rightTriggerIsTriggered = false;
	bool canSteer = false;

    public Transform rightHandTransform;
    public Transform leftHandTransform;
    public Transform wheel;

	public float Wheel_Rotate_Speed_Multiplier = 300;
	public float Wheel_Pull_Speed_Multiplier = 240;
    public float Velocity_Multiplier;

    private Vector3 OriginalGrabPosition;
    private Vector3 NewGrabPosition;
    private Vector3 wheelStartPos;
    private Vector3 saveGrabPos;

    private Rigidbody rb;

    private float wheelRotation;
    private float wheelDistance;
    private float velocity = 0;


    //Changed by LH_Listener to 0 if force is positive or 1 if negative 
    public int LH_Grip_Pressed = 0;
    public int LH_Trigger_Pressed = 0;

    void Start () {
        //Set original wheel Position
        wheelStartPos = wheel.position;

		rb = GetComponent<Rigidbody>();

        //rb.velocity = Vector3.zero;

    }

    void Update () {

        if (canSteer)
        {
            NewGrabPosition = rightHandTransform.position;
            UpdatePitch();
            UpdateRoll();
            Move();
            Accelerate();
        }

        //rb.velocity = transform.forward*Time.deltaTime*Test_Speed;
        //transform.position += transform.forward * Time.deltaTime * Test_Speed;
	}

    public void Laser()
    {

    }

	public void Move(){

        //Move forward based on current velocity
        transform.position += transform.forward * Time.deltaTime * velocity;

        //Rotate around z and y axis depending on position of steering wheel
        transform.RotateAround(transform.position, transform.forward, wheelRotation*Time.deltaTime*Wheel_Rotate_Speed_Multiplier);
		transform.RotateAround(transform.position, transform.right, wheelDistance*Time.deltaTime*Wheel_Pull_Speed_Multiplier);
	}


    public void Accelerate()
    {

        //If left hand trigger is pressed accelerate the ship
        if (leftHandleIsTriggered == true)
        {
            velocity += Time.deltaTime * Velocity_Multiplier;
        }

    }

    public void UpdatePitch()
    {    
        //Determine new wheel position based on clamped position values of the right hand
        wheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, Mathf.Clamp(rightHandTransform.localPosition.z, -0.3f+wheelStartPos.z, 0.3f+wheelStartPos.z));
        
        //Update displacement from hand to original wheel position
        wheelDistance = wheel.localPosition.z-wheelStartPos.z;

    }

    public void UpdateRoll()
    {
        //Get (1,0,0) vector and the vector from the wheel center to right hand
         Vector3 vector1 = transform.right;
         Vector3 vector2 = NewGrabPosition - wheel.position;

         float degreeBetween = Vector3.Angle(vector1, vector2);

        //Calculate cross product to determine polarity of angle
         Vector3 cross = Vector3.Cross(vector1, vector2);
         if (Vector3.Angle(cross, transform.forward)>90f)
         {
            degreeBetween = -degreeBetween;
	     }
			wheelRotation = degreeBetween;

         //Rotate wheel gameObject
         wheel.eulerAngles = new Vector3(0, 0, degreeBetween) + transform.eulerAngles;
    }
    

	public void HandleTriggered(int handleNum, bool isTriggered){

        //handleNum = 0 for left hand
        //handleNum = 1 for right hand
        
        //isTriggered is true when Trigger is pressed in

        if (handleNum==0)
        {
		    leftHandleIsTriggered = isTriggered;
		}
        else
        {
			rightHandleIsTriggered = isTriggered;
        }
        CheckSteering();
	}

	public void TriggerTriggered(int handNum, bool isTriggered){

        //handleNum = 0 for left hand
        //handleNum = 1 for right hand

        //isTriggered is true when Grip is pressed in

        if (handNum == 0)
        {
            //Run if left hand grip is grabbed or released
            leftTriggerIsTriggered = isTriggered;
        }
        else
        {
            //Run if right hand grip is grabbed or release
            rightTriggerIsTriggered = isTriggered;
        }
		    CheckSteering();
	}

	public void CheckSteering(){
        //canSteer = leftHandleIsTriggered && rightHandleIsTriggered && leftTriggerIsTriggered && rightTriggerIsTriggered;
        if (!canSteer){
            OriginalGrabPosition = rightHandTransform.localPosition;
		    if(rightHandleIsTriggered && rightTriggerIsTriggered){
			    VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(rightHandTransform.gameObject), 1f, 1f, 1f);
		    }
        }else{
		    if(!(rightHandleIsTriggered && rightTriggerIsTriggered)){
			   VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(rightHandTransform.gameObject), 1f, 2f, 1f);
		    }
	    }
		canSteer = rightHandleIsTriggered && rightTriggerIsTriggered;
	}


}
