using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shipMovement : MonoBehaviour {

    public Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    //Recieves float of wheel start Position - local hand position Z-AXIS and moves ship forward accordingly
    public void shipForward(float handPosition)
    {
        //float xVelocity = handPosition*Mathf.Pow(9.845f,handPosition);
        float thrust = -0.05f;
        //-0.3 to 0.3
        rb.AddForce(transform.right * thrust);
       
      

        //rotate up or down 

        transform.eulerAngles = new Vector3(0, 0, 200 * handPosition);

    }


}
