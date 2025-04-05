using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    public int maxRebounds;
    int currentRebounds;
    EffectHandler effectHandler;
    public EffectData reboundEffect;
    void Start()
    {
        effectHandler = gameObject.AddComponent<EffectHandler>();
        currentRebounds = maxRebounds;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentRebounds <= 0) {
            GetComponent<Damager>().DestoryDamager();
            return;
        }
        currentRebounds--;
        effectHandler.CreateEffect(reboundEffect, transform.position, Quaternion.identity);
    }
}
