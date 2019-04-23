using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineController : baseItem {

    public float PRIME_DELAY;
    public float EXPLOSION_DURATION;

    private bool isPrimed;

	void Start () {
        isPrimed = false;
        Invoke("PrimeMine", PRIME_DELAY);    		
	}
	
    void PrimeMine()
    {
        isPrimed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPrimed && CheckIsShip(other))
        {
            ActivateExplosion();
            DamageShip(other);
            Invoke("DestroySelf", EXPLOSION_DURATION);
        }
    }

    private void ActivateExplosion()
    {
        isPrimed = false;
        // if you need to activate particle system stuff do it here
    }
}
