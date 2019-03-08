using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

public class PlayerScript : MonoBehaviour {

		  //True when left right hand enters rightHandle
		bool leftHandleIsTriggered = false;
		bool rightHandleIsTriggered = false;

        //True when trigger is pressed
		public bool leftTriggerIsTriggered = false;
		public bool rightTriggerIsTriggered = false;

		//True when grib is pressed.
		public bool leftGripIsTriggered = false;
		public bool rightGripIsTriggered = false;

		//True when all conditions for steering are met
		bool canSteer = false;

		public Transform rightHandTransform;
		public Transform leftHandTransform;
		public Transform wheel;
		public Transform midwheel;
		public Transform inwheel;

		public float Wheel_Rotate_Speed_Multiplier = 300;
		public float Wheel_Pull_Speed_Multiplier = 240;
		public float Acceleration = 10;
		public float Max_Velocity = 15;

		private Vector3 OriginalGrabPosition;
		private Vector3 NewGrabPosition;
		private Vector3 wheelStartPos;
		private Vector3 saveGrabPos;

		private Rigidbody rb;

		private float wheelRotation;
		private float wheelDistance;
		public float velocity = 0;


		//Changed by LH_Listener to 0 if force is positive or 1 if negative
		public int LH_Grip_Pressed = 0;
		public int LH_Trigger_Pressed = 0;
        public int ActiveItem = -1;
    
        
         

        private GameManagerScript gm;

    void Start () {
        //Set original wheel Position
        wheelStartPos = wheel.position;

				rb = GetComponent<Rigidbody>();
				gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();


    }

    void Update ()
    {

        if (canSteer)
        {
            NewGrabPosition = rightHandTransform.position;
            UpdatePitch();
            UpdateRoll();
            rotate();


            //Activate which ever item is enabled

            //GameObject.GetComponent<itemsScript>().activeItem();
        }

        Accelerate();
        move();
    }

 


    private void move()
    {
        //Move forward based on current velocity
        transform.position += transform.forward * Time.deltaTime * velocity;
    }

		private void rotate(){

		      //Create a rotate multiplier based on velocity mapped to 1 to 0
		      float rotateMultiplier = 0;
		      rotateMultiplier = velocity / Max_Velocity;
					rotateMultiplier = 1;

		      //Rotate around z and y axis depending on position of steering wheel
		      transform.RotateAround(transform.position, transform.forward, wheelRotation*Time.deltaTime*Wheel_Rotate_Speed_Multiplier*rotateMultiplier);
					transform.RotateAround(transform.position, transform.right, wheelDistance*Time.deltaTime*Wheel_Pull_Speed_Multiplier*rotateMultiplier);
		}

    public void Accelerate()
    {

        if (canSteer)
        {
            //If left hand trigger is pressed accelerate the ship
            if (leftGripIsTriggered == true)
            {
                if (velocity < Max_Velocity)
                {
                    velocity += Time.deltaTime * Acceleration;
                }else{
										velocity = Max_Velocity;
								}
            }
        }
        if (leftGripIsTriggered == false)
        {
            if(velocity > 0)
            {
                velocity -= Time.deltaTime * Acceleration;
            }
        }

    }

    public void UpdatePitch()
    {
        //Determine new wheel position based on clamped position values of the right hand
				wheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, Mathf.Clamp(rightHandTransform.localPosition.z, -0.3f+wheelStartPos.z, 0.3f+wheelStartPos.z));
				midwheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, Mathf.Clamp(rightHandTransform.localPosition.z, -0.3f+wheelStartPos.z, 0.3f+wheelStartPos.z));
        inwheel.localPosition = new Vector3(wheel.localPosition.x, wheel.localPosition.y, Mathf.Clamp(rightHandTransform.localPosition.z, -0.3f+wheelStartPos.z, 0.3f+wheelStartPos.z));

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
				midwheel.eulerAngles = new Vector3(0, 0, degreeBetween*2/3) + transform.eulerAngles;
				inwheel.eulerAngles = new Vector3(0, 0, degreeBetween/3) + transform.eulerAngles;
    }


		public void HandleTriggered(int handleNum, bool isTriggered){

	      //handleNum = 0 for left hand
	      //handleNum = 1 for right hand

	      //isTriggered is true when right hand enters the right steering wheel grip collider

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

		public void GripTriggered(int handNum, bool isTriggered){

        //handleNum = 0 for left hand
        //handleNum = 1 for right hand

        //isTriggered is true when Grip is pressed in

    		if (handNum == 0)
        {
            //Run if left hand grip is grabbed or released
            leftGripIsTriggered = isTriggered;
        }
        else
        {
            //Run if right hand grip is grabbed or release
            rightGripIsTriggered = isTriggered;
        }
		}

    public void CheckSteering(){

        //Determine if the steering is activated and can be controller
        if (!canSteer)
        {
            OriginalGrabPosition = rightHandTransform.localPosition;

				    if(rightHandleIsTriggered && rightTriggerIsTriggered)
		            {
		                //Vibrate Controller
					    			VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(rightHandTransform.gameObject), 1f, 1f, 1f);
				    		}
        }
        else
        {
				    if(!(rightHandleIsTriggered && rightTriggerIsTriggered))
		            {
		                //Vibrate Controller
		                VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(rightHandTransform.gameObject), 1f, 2f, 1f);
				    }
	    	}
				canSteer = rightHandleIsTriggered && rightTriggerIsTriggered && gm.canGo();
		}

		public void ShowEndScoreboard(float compTime){
				transform.Find("Scoreboard").GetComponent<TextMeshPro>().SetText("Time: " + compTime.ToString("F1"));
		}


}
