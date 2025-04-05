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

    [SerializeField] private float YSpeedCap, YFallSpeedCap, YFloatSpeedCap = 5f;
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

        if(inputVertical <= -0.1f) currentYSpeedCap = YFloatSpeedCap;
        if(inputVertical >= 0.1f) currentYSpeedCap = YFallSpeedCap;

        if (Mathf.Abs(rb.velocity.y) >= currentYSpeedCap)
        {
            targetYVelocity = Mathf.Lerp(
                velocityY,
                currentYSpeedCap * Mathf.Sign(rb.velocity.y),
                Time.fixedDeltaTime * YAcceleration);
        }

        //Update Velocity
        rb.velocity = new Vector2(targetXVelocity, targetYVelocity);

        Debug.Log(rb.velocity);
    }
}
