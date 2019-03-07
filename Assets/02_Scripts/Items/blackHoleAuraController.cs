using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleAuraController : baseItem {

    public float BLACK_HOLE_FORCE_MULTIPLIER = 1f;
    public float BLACK_HOLE_STRENGTH;

    private void OnTriggerStay(Collider other)
    {
        if (CheckIsShip(other))
            ApplyForce(other);
    }
    
    private void ApplyForce(Collider other)
    {
        Rigidbody shipRigidbody = other.GetComponent<Rigidbody>();
        Vector3 shipToCore = ShipToCoreVector(other);
        float distanceSquared = shipToCore.sqrMagnitude;
        if (distanceSquared < .1f)
            distanceSquared = .1f;
        float forceMultiplier = BLACK_HOLE_FORCE_MULTIPLIER * BLACK_HOLE_STRENGTH / distanceSquared;
        shipRigidbody.AddForce(forceMultiplier * -shipToCore.normalized);
    }
    
    private Vector3 ShipToCoreVector(Collider other)
    {
        Vector3 shipPosition = other.gameObject.transform.position;
        Vector3 corePosition = transform.parent.transform.position;
        Vector3 shipToCore = shipPosition - corePosition;
        return shipToCore;
    }
}
