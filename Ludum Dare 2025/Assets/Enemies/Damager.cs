using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [HideInInspector]
    EffectHandler effectHandler;
    public EffectData destroyEffect;
    bool destroyOnWorldCollision = true;
    void Start()
    {
        effectHandler = gameObject.AddComponent<EffectHandler>();
        if (GetComponent<Rebound>() != null) destroyOnWorldCollision = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || (collision.tag == "World" && destroyOnWorldCollision)) {
            DestoryDamager();
        }
    }

    public void DestoryDamager() {
        effectHandler.CreateEffect(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
