using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class baseItem : MonoBehaviour {

    public float DAMAGE = 0;
    public GameObject parentShip = null;
    
    virtual protected bool CheckIsShip(Collider other)
    {
        return (other.tag == "Player" || other.tag == "AI");
    }

    virtual protected bool CheckIsSelf(Collider other)
    {
        return (parentShip == other.gameObject);
    }

    virtual protected void DamageShip(Collider other)
    {
        if(CheckIsShip(other))
        {
            shipHealthManager shipHealth = other.gameObject.GetComponent<shipHealthManager>();
            if (shipHealth != null)
                shipHealth.DamageShip(DAMAGE);
        }
    }

    virtual protected void DestroySelf()
    {
        Destroy(gameObject);
    }

    virtual public GameObject setParentShip(GameObject desiredParent)
    {
        parentShip = desiredParent;
        return parentShip;
    }
}
