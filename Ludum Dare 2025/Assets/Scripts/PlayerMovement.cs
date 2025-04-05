using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool Attacking { get; private set; } = false;
    [SerializeField] private Rigidbody2D rb;
    [Header("Player Stats")]
    [SerializeField] private float XSpeed = 5f;
    [SerializeField] private float XAcceleration = 3f;

    [Header("Vertical Movement")]
    [SerializeField] private float YSpeedCap = 3.7f;
    [SerializeField] private float YFallSpeedCap  = 7f;
    [SerializeField] private float YFloatSpeedCap = 2.6f;
    [SerializeField] private float Gravity = -9.8f;
    [SerializeField] private float YAcceleration = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    float inputHorizontal = 0;
    float inputVertical = 0;
    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        //Handle Horizontal Movement
        float targetAcceleration = XAcceleration;

        float targetXVelocity = inputHorizontal * XSpeed;

        float velocityX = rb.velocity.x;
        targetXVelocity = Mathf.Lerp(
            velocityX,
            targetXVelocity,
            Time.fixedDeltaTime * XAcceleration);

        //Handle Vertical Movement
        float targetYVelocity = rb.velocity.y + Gravity * Time.fixedDeltaTime;
        float velocityY = rb.velocity.y;

        float currentYSpeedCap = YSpeedCap;

        if(inputVertical <= -0.1f) currentYSpeedCap = YFallSpeedCap;
        if(inputVertical >= 0.1f) currentYSpeedCap = YFloatSpeedCap;

        if (Mathf.Abs(rb.velocity.y) >= currentYSpeedCap)
        {
            targetYVelocity = Mathf.Lerp(
                velocityY,
                currentYSpeedCap * Mathf.Sign(rb.velocity.y),
                Time.fixedDeltaTime * YAcceleration);
        }

        //Update Velocity
        rb.velocity = new Vector2(targetXVelocity, targetYVelocity);

        //Update Rotation
        float targetAngle = -180/(2*Mathf.PI)*Mathf.Atan(targetXVelocity/targetYVelocity); //includes converstion to degrees
        transform.eulerAngles = new Vector3(0,0,targetAngle);
    }
}
