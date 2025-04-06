using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [HideInInspector]
    public EffectHandler effectHandler;
    public Collider2D hitbox;
    public virtual void Start()
    {
        effectHandler = gameObject.AddComponent<EffectHandler>();
    }
    public void OnDeath() {
        effectHandler.CreateEffect(enemyData.deathEffect, transform.position, Quaternion.identity);
        StopAllCoroutines();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && other.gameObject.GetComponent<PlayerMovement>().Attacking) {
            OnDeath();
        }
        if (other.tag == "PlayerDamager") {
            OnDeath();
        }
    }
}
