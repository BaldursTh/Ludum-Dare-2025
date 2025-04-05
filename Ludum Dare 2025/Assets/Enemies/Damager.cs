using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [HideInInspector]
    EffectHandler effectHandler;
    public EffectData destroyEffect;
    void Start()
    {
        effectHandler = gameObject.AddComponent<EffectHandler>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            effectHandler.CreateEffect(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
