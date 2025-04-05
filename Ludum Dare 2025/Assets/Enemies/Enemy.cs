using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [HideInInspector]
    public EffectHandler effectHandler;
    public virtual void Start()
    {
        effectHandler = gameObject.AddComponent<EffectHandler>();
    }
    public void OnDeath() {
        effectHandler.CreateEffect(enemyData.deathEffect, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            OnDeath();
        }
    }
}
