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
    [SerializeField] private float YFallSpeedCap = 7f;
    [SerializeField] private float YFloatSpeedCap = 2.6f;
    [SerializeField] private float Gravity = -9.8f;
    [SerializeField] private float YAcceleration = 50f;

    [Header("Dashes")]
    [SerializeField] private int MaxDashes = 3;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float currentDashTime = 0.3f;
    private int dashDirection = 1;
    public int CurrentDashes { get; private set; } = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentDashes = MaxDashes;
    }

    float inputHorizontal = 0;
    float inputVertical = 0;
    bool dashing = false;

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (dashing) return;
        if (inputHorizontal <= -0.1f) dashDirection = -1;
        if (inputHorizontal >= 0.1f) dashDirection = 1;

        if (Input.GetButtonDown("Jump") && CurrentDashes > 0)
        {
            CurrentDashes--;
            dashing = true;
            currentDashTime = dashTime;
        }
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

        //Handle Dash
        if (dashing)
            targetXVelocity = dashSpeed * dashDirection;

        currentDashTime -= Time.fixedDeltaTime;

        if (currentDashTime < 0f)
        {
            dashing = false;
        }

        //Handle Vertical Movement
        float targetYVelocity = rb.velocity.y + Gravity * Time.fixedDeltaTime;
        float velocityY = rb.velocity.y;

        float currentYSpeedCap = YSpeedCap;

        if (inputVertical <= -0.1f) currentYSpeedCap = YFallSpeedCap;
        if (inputVertical >= 0.1f) currentYSpeedCap = YFloatSpeedCap;

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
        float targetAngle = -180 / (2 * Mathf.PI) * Mathf.Atan(targetXVelocity / targetYVelocity); //includes converstion to degrees
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }
}
