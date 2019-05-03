using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    private float Max_Velocity = 150f;
    private float velocity = 5.0f;
    private float actingMaxVelocity = 0;
    private float acceleration = 10f;
    private int currHoop = -1;
    private float shipRotation;
    private float shipDistance;

    public bool raceHasStarted = false;

    private GameManagerScript gm;
    private itemPrefabSpawnController itemController;
    private Transform hoop;

    //set RB's, Start Position, Hoops and GM
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        hoop = gm.getNextHoop(currHoop);
        currHoop++;
        itemController = gameObject.GetComponent<itemPrefabSpawnController>();
    }
    //uppercase GameObject is type
    //lowercase gameObject is the game object that script is attached to 
	void Update () {
    if(raceHasStarted){

      itemController.ActivateItem();

      Vector3 delta = hoop.position - transform.position;
      Vector3 dxf = Vector3.Cross(delta, transform.forward);
      Vector3 newUp = Vector3.Cross(delta, dxf);

      float af = Vector3.SignedAngle(transform.up, newUp, transform.forward);
      if(af>90f){
        af-= 180f;
      }else if(af<-90f){
        af+=180f;
      }

      //need to call generate random vector only when ship hits new hoop and until hits new hoop keep adding that to the delta vector
      //keep adding b/c every update will overwrite previous vector 


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

    public Vector3 GenerateRandVector()
    {
        Vector3 rand_vector;
        float rand_f;
        float rand_r;
        float rand_u;
        float decide = Random.value;
        if (decide >= .5)
        {
            rand_f = Random.value * 90;
        }
        else
        {
            rand_f = Random.value * -90;
        }
        decide = Random.value;
        if (decide >= .5)
        {
            rand_r = Random.value * 180;//need to review this b/c want this b/w 0 and 180 
            //but need to ensure can still obtain random value less than 180 
        }


        if (decide >= .5)
        {
            rand_u = Random.value * 180;
        }
        else
        {
            rand_u = Random.value * -180;
        }
        rand_vector = (rand_f, rand_r, rand_u);

        return rand_vector; 
    }
}
