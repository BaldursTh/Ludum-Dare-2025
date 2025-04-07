using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    float moveSpeed;
    public float baseSpeed;
    Rigidbody2D rb;
    EnemyAnimations anim;
    bool collide = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<EnemyAnimations>();
        moveSpeed = baseSpeed;
        cooldown = CollideCooldown();
    }
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collide) return;
        if (collision.tag == "World" || collision.tag == "Edge") {
            StartCollideCooldown();
            anim.FlipX(2);
            moveSpeed = -moveSpeed;
        }
    }

    void StartCollideCooldown() {
        StopCoroutine(cooldown);
        cooldown = CollideCooldown();
        StartCoroutine(cooldown);
    }
    IEnumerator cooldown;
    IEnumerator CollideCooldown() {
        collide = false;
        yield return new WaitForSeconds (1f);
        collide = true;
    }
}
