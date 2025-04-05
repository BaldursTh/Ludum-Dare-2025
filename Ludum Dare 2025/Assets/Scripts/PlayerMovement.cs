using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool Attacking {get; private set;} = false;
    [SerializeField] private Rigidbody2D rb;
    [Header("Player Stats")]
    [SerializeField] private float HSpeed = 5f;
    [SerializeField] private float HAcceleration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    float inputHorizontal = 0;
    float inputVertical = 0;
    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        //Hande Horizontal Movement
        float targetSpeed = HSpeed;
        float targetAcceleration = HAcceleration;

        Vector2 targetVelocity = new Vector2(inputHorizontal,0)*HSpeed;



        //Update Velocity
        Vector2 velocity = rb.velocity;

            rb.velocity = Vector2.Lerp(
                velocity,
                targetVelocity,
                Time.fixedDeltaTime * targetAcceleration);
    }
}
