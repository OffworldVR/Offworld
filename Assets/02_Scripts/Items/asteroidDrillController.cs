using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidDrillController : baseItem {
    
    public float DURATION;
    public float SPEED_MODIFIER;
    private GameObject parentShip;

	void Start () {
        //be sure to instantiate as a child
        parentShip = transform.parent.gameObject;

        // TODO: make ship invincible
        // possible code
        // parentShip.GetComponent<ShipStats>().isInvincible = true;

        // TODO: make ship have a higher top speed
        // possible code
        // parentShip.GetComponent<ShipStats>().maxSpeedModifier = SPEED_MODIFIER;

        Invoke("DestroySelf", DURATION);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (CheckIsShip(other) && other.gameObject != parentShip)   
            DamageShip(other);        
    }
    

    protected override void DestroySelf()
    {
        // TODO: make ship vincible
        // possible code
        // parentShip.GetComponent<ShipStats>().isInvincible = false;

        // TODO: make ship have a normal top speed
        // possible code
        // parentShip.GetComponent<ShipStats>().maxSpeedModifier = 1f;

        Destroy(gameObject);
    }
}
