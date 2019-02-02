using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shipMovement : MonoBehaviour {

    public Rigidbody rb;




    public int forwardOrBack = 0;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        shipAccelerate();
	}

    //Passed a positive int for forward, 0 for stop, -1 for reverse
    public void shipAccelerate()
    {
        float thrust = 0.5f;

        Vector3 force = new Vector3(0, 0, 0);

        if(forwardOrBack == 1)
        {
            force = transform.forward * thrust;
        }
        else if(forwardOrBack == -1)
        {

            force = -transform.forward * thrust;

        }

        //Add Force
        rb.AddForce(force);
       

    }

    //Passed value of wheel rotation
    public void shipRoll(float rotateValue)
    {
        //transform.eulerAngles = new Vector3(0, 0, 200 * handPosition);


        //transform.eulerAngles = new Vector3(0, 0, rotateValue);
        transform.RotateAround(transform.position, transform.forward, rotateValue * Time.deltaTime);

    }

    //Recieves float of wheel start Position - local hand position Z-AXIS and moves ship forward accordingly
    public void shipPitch(float deltaDistance)
    {
        Debug.Log(deltaDistance);

        deltaDistance = deltaDistance * -100f;
        transform.RotateAround(transform.position, transform.right, deltaDistance * Time.deltaTime);

    }

}
