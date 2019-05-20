using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipHealthManager : MonoBehaviour {

    const float DEATH_TIME = 10f;
    const float MAX_HEALTH = 100f;
    const float MIN_HEALTH = 0f;
    [SerializeField]
    float health;

    private void Start()
    {
        ResetHealth();
    }

    private void Update()
    {
        if (IsDead())
        {
            HandleDeath();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (health / MAX_HEALTH);
    }

    public float ResetHealth()
    {
        health = MAX_HEALTH;
        return GetHealth();
    }

    public float ModifyHealth(float displacement)
    {
        health = health + displacement;
        health = Mathf.Clamp(health, MIN_HEALTH, MAX_HEALTH);
        return GetHealth();
    }

    public float ModifyHealthPercent(float displacement)
    {
        ModifyHealth(MAX_HEALTH * displacement / 100);
        health = Mathf.Clamp(health, MIN_HEALTH, MAX_HEALTH);
        return GetHealthPercent();
    }

    public float DamageShip(float amount)
    {
        return ModifyHealth(-amount);
    }

    public float HealShip(float amount)
    {
        return ModifyHealth(amount);
    }

    public bool IsFull()
    {
        return health >= MAX_HEALTH;
    }

    public bool IsDead()
    {
        return health <= MIN_HEALTH;
    }

    private void HandleDeath()
    {
        FreezePosition();
        Invoke("UnfreezePosition", DEATH_TIME);
        Invoke("ResetHealth", DEATH_TIME);
    }

    private void FreezePosition()
    {
        if (gameObject.tag == "Player")
            gameObject.GetComponent<PlayerScript>().DisableMovement();
        else if (gameObject.tag == "AI")
            gameObject.GetComponent<AI>().DisableMovement();
    }

    private void UnfreezePosition()
    {
        if (gameObject.tag == "Player")
            gameObject.GetComponent<PlayerScript>().EnableMovement();
        else if (gameObject.tag == "AI")
            gameObject.GetComponent<AI>().EnableMovement();
    }
}
