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
    private float health = 100.0f;
    private float decceleration = 5.0f;
    private float shipDistance;
    private int shipSelection;

    public bool raceHasStarted = false;
    private GameManagerScript gm;
    private Transform hoop;

    //set RB's, Start Position, Hoops and GM
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        hoop = gm.getNextHoop(currHoop);
        currHoop++;
        Debug.Log(acceleration);
    }
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
                //  velocity -= Time.deltaTime*acceleration;
                velocity -= Time.deltaTime * decceleration;
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
    
    public float getAcceleration()
    {
        return acceleration;
    }
    public void setAcceleration(float accel)
    {
        acceleration = accel;
    }

    public float getDecceleration()
    {
        return decceleration;
    }
    public void setDecceleration(float deccel)
    {
        decceleration = deccel;
    }


    public float getHealth()
    {
        return health;
    }
    public void setHealth(float heal)
    {
        health = heal;
    }

    public float getMaxVelocity()
    {
        return Max_Velocity;
    }
    public void setMaxVelocity(float MaxV)
    {
        Max_Velocity = MaxV;
    }

    public float getRotation()
    {
        return rotation;
    }
    public void setRotation(float rotate)
    {
        rotation = rotate;
    }
}
