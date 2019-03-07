using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guidedMissileController : baseItem {

    public float TRAVEL_SPEED;
    public float DURATION;

    public GameObject targetShip;

	void Start () {
        GetTargetShip();
        InvokeRepeating("Update60", 0, 1f / 60f);
        Invoke("DestroySelf", DURATION);
	}

    void GetTargetShip()
    {
        // TODO: 
        // needs to be implemented depending on how the ship's placing script is implemented
        // Possible implementation of this function
        // int currentPlace = other.GetComponent<ShipTracker>().getShipPlace(gameObject.transform.parent);
        // targetShip = other.GetComponent<ShipTracker>().getShipAtPlace(int currentPlace - 1);
        // note condition when already in first
    }

    void Update60()
    {
        MoveTowardTarget();
        PointAtTarget();
    }

    void MoveTowardTarget()
    {
        Vector3 targetPosition = targetShip.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, TRAVEL_SPEED);
    }

    void PointAtTarget()
    {
        transform.LookAt(targetShip.transform);
        transform.Rotate(transform.forward, 90);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckIsShip(other))
            DamageShip(other);
        DestroySelf();
    }
}
