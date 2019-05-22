using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    private float Max_Velocity = 150f;
    private float velocity = 5.0f;
    private float actingMaxVelocity = 0;
    private float acceleration = 10f;
    private int currHoop = -1;
    private float rotation = 10.0f;
    private float shipDistance;
    private int shipSelection;

    public bool raceHasStarted = false;
    public float[] shipMaxVelocity   =  {150, 200, 200, 150, 100};
    public float[] shipAcceleration  =  { 10,   8,   5,  15,  20};
    public float[] shipRotation      =  { 10,   7,  15,  10,   6};
    public float[] shiphealth        =  { 80,  90, 150, 100,  50};
    public float[] shipDecceleration =  {  8,   6,   5,  10,   5};

    private GameManagerScript gm;
    private Transform hoop;

    //set RB's, Start Position, Hoops and GM
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        hoop = gm.getNextHoop(currHoop);
        currHoop++;

        //fox ship = 0
        //x-wing = 1
        //vertical = 2
        //saucer = 3
        //pod racer = 4

        Max_Velocity = shipMaxVelocity[gm.getShipValue()];
        acceleration = shipAcceleration[gm.getShipValue()];
        rotation = shipRotation[gm.getShipValue()];

        Debug.Log(gm.getShipValue());
        Debug.Log(Max_Velocity);
        Debug.Log(acceleration);
        Debug.Log(rotation);


    }
    //add deccelertion value and health value to ship array
    //add into GM script ai selction of ships based on player pref
    //instead of slowin down by subtraction acceleration substitute it with decceleration values
    //NEED:
    //implement decceleration
    

    void Update () {
    if(raceHasStarted){
      Vector3 delta = hoop.position - transform.position;
      Vector3 dxf = Vector3.Cross(delta, transform.forward);
      Vector3 newUp = Vector3.Cross(delta, dxf);

      float af = Vector3.SignedAngle(transform.up, newUp, transform.forward);
      if(af>90f){
        af-= 180f;
      }else if(af<-90f){
        af+=180f;
      }

      transform.RotateAround(transform.position, transform.forward, Time.deltaTime*af*2);

      float ar = Vector3.SignedAngle( transform.forward,delta, transform.right);

      transform.RotateAround(transform.position, transform.right, Time.deltaTime*ar);

      float r = ((180-Mathf.Abs(ar))/180f);
      actingMaxVelocity = Max_Velocity*r;
      if(velocity<actingMaxVelocity){
        velocity += Time.deltaTime*acceleration;
        if(velocity>Max_Velocity){
          velocity = Max_Velocity;
        }
      }else{
        velocity -= Time.deltaTime*acceleration;
        if(velocity<0f){
          velocity = 0f;
        }
      }
      transform.position += transform.forward * Time.deltaTime * velocity;

      }
  }

    public void HitHoop(int hoopNum)
    {
        if (hoopNum == currHoop)
        {
            hoop = gm.GetHoopWithNum(gm.FindNextHoop(hoopNum));
            currHoop = hoop.GetComponent<HoopScript>().hoopNum;
        }
        //Debug.Log(hoopNum);
    }
}
