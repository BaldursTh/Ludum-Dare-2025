using System;
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
    public GameObject player;
    public virtual void Awake()
    {
        effectHandler = gameObject.AddComponent<EffectHandler>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnDeath() {
        effectHandler.CreateEffect(enemyData.deathEffect, transform.position, Quaternion.identity);
        player.GetComponent<PlayerMovement>().AddDash();
        StopAllCoroutines();
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player" && (other.gameObject.GetComponent<PlayerMovement>().Attacking ||other.gameObject.GetComponent<PlayerMovement>().getDash())) {
            OnDeath();
        }
        if (other.tag == "PlayerDamager") {
            OnDeath();
        }
    }

    public virtual void Update()
    {
        if (transform.position.y - transform.lossyScale.y > Camera.main.transform.position.y + Camera.main.orthographicSize) {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
