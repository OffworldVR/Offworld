using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserController : baseItem {
    
    public float TRAVEL_SPEED;
    public float DURATION;

    void Start () {
        InvokeRepeating("Update60", 0, 1f / 60f);
        Invoke("DestroySelf", DURATION);
    }
	
	void Update60 ()
    {
        transform.Translate(TRAVEL_SPEED * transform.forward);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (CheckIsShip(other))
            DamageShip(other);
        DestroySelf();
    }
}
