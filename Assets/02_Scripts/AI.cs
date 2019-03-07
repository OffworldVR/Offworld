using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public float Max_Velocity = 15;
    public float velocity = 5.0f;

    private int currHoop = -1;
    private float shipRotation;
    private float shipDistance;

    private GameManagerScript gm;
    private Transform hoop;

    //set RB's, Start Position, Hoops and GM
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        hoop = gm.getNextHoop(currHoop);
        currHoop++;
    }

	//Every Frame need to rotate ship, accelerate ship and then move ship - idk if need pitch and roll for AI... perhaps
	void Update () {
      Vector3 delta = hoop.position - transform.position;
      Vector3 dxf = Vector3.Cross(delta, transform.forward);
      Vector3 newUp = Vector3.Cross(delta, dxf);

      float af = Vector3.SignedAngle(transform.up, newUp, transform.forward);
        //smooth out angles, look at the negative angles so does not need to do full revolution but instead can stay upside down or turn other direction
        //add random vetor later to add unpredicatabiltiy and make more unique movement

      transform.RotateAround(transform.position, transform.forward, Time.deltaTime*af);

      float ar = Vector3.SignedAngle( transform.forward,delta, transform.right);

      transform.RotateAround(transform.position, transform.right, Time.deltaTime*ar);

      transform.position += transform.forward * Time.deltaTime * velocity;
    }

    public void HitHoop(int hoopNum)
    {
        if (hoopNum == currHoop)
        {
            hoop = gm.getNextHoop(hoopNum);
            currHoop = hoop.GetComponent<HoopScript>().hoopNum;
        }
        Debug.Log(hoopNum);
    }
}
