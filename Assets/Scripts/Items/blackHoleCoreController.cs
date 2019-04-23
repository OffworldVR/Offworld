using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleCoreController : baseItem {

    private GameObject blackHoleAura;

    public float DURATION;
    public float AURA_DELAY;
    public float TRAVEL_SPEED;

    private void Start()
    {
        blackHoleAura = GetComponentInChildren<blackHoleAuraController>(true).gameObject;
        InvokeRepeating("Update60", 0, 1f / 60f);
        Invoke("ActivateAura", AURA_DELAY);
        Invoke("DestroySelf", DURATION);
    }

    private void Update60()
    {
        transform.Translate(TRAVEL_SPEED * transform.forward);
    }

    private void ActivateAura()
    {
        CancelInvoke("Update60");
        blackHoleAura.SetActive(true);
    }
}
