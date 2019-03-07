using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class baseItem : MonoBehaviour {

    public float DAMAGE = 0;

    virtual protected bool CheckIsShip(Collider other)
    {
        return (other.tag == "Player" || other.tag == "Ship");
    }

    virtual protected void DamageShip(Collider other)
    {
        // TODO: 
        // needs to be implemented depending on how the ship's (health) script is implemented
        // Possible implementation of this function
        // other.GetComponent<ShipData>().dealDamage(DAMAGE);
    }

    virtual protected void DestroySelf()
    {
        Destroy(gameObject);
    }
}
