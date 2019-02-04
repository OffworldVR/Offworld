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

		public float WHEEL_ROTATE_SPEED_MULTIPLIER;
		public float WHEEL_PULL_SPEED_MULTIPLIER;
		public float TEST_SPEED;

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
            UpdatePitch();
            UpdateRoll();
						Move();
						//TODO REMOVE THIS
        }
				// rb.velocity = transform.forward*Time.deltaTime*TEST_SPEED;
				transform.position += transform.forward*Time.deltaTime*TEST_SPEED;
	}

	public void Move(){
		transform.RotateAround(transform.position, transform.forward, wheelRotation*Time.deltaTime*WHEEL_ROTATE_SPEED_MULTIPLIER);
		transform.RotateAround(transform.position, transform.right, wheelDistance*Time.deltaTime*WHEEL_PULL_SPEED_MULTIPLIER);
	}

  public void UpdatePitch()
  {
      //float distanceBetween = OriginalGrabPosition.z - NewGrabPosition.z;

      //wheel.localPosition = wheelStartPos + new Vector3(0,0, -distanceBetween);
      wheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, Mathf.Clamp(rightHandTransform.localPosition.z, -0.3f+wheelStartPos.z, 0.3f+wheelStartPos.z));
			wheelDistance = wheel.localPosition.z-wheelStartPos.z;
      // Debug.Log(distanceBetween);

      //Call script to move ship forward
      // shipScript.shipForward(wheelStartPos.z-rightHandTransform.localPosition.z);
  }

  public void UpdateRoll()
  {
      //Calculate angle between 4 points
      // Vector3 vector1 = new Vector3(OriginalGrabPosition.x, OriginalGrabPosition.y, 0) - new Vector3(wheelStartPos.x, wheelStartPos.y, 0);
			Vector3 vector1 = transform.right;
      Vector3 vector2 = NewGrabPosition - wheel.position;

      // saveGrabPos = rightHandTransform.position;

      float degreeBetween = Vector3.Angle(vector1, vector2);

      //Calculate cross product to determine polarity of angle
      Vector3 cross = Vector3.Cross(vector1, vector2);
      if (Vector3.Angle(cross, transform.forward)>90f){
				degreeBetween = -degreeBetween;
			}
			wheelRotation = degreeBetween;

      //Rotate physical wheel
      wheel.eulerAngles = new Vector3(0, 0, degreeBetween) + transform.eulerAngles;
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
        OriginalGrabPosition = rightHandTransform.localPosition;
				// wheelStartPos = wheel.localPosition;
    }
		// canSteer = rightHandleIsTriggered && rightTriggerIsTriggered;
		canSteer = rightHandleIsTriggered && rightTriggerIsTriggered;
	}


}
