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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<EnemyAnimations>();
        moveSpeed = baseSpeed;
    }
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "World" || collision.tag == "Edge") {
            anim.FlipX(2);
            moveSpeed = -moveSpeed;
        }
    }
}
