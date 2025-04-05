using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    [Header("Damage")]
    [SerializeField] private float damageFlingY = 5f;
    [SerializeField] private float damageFlingX = 9f;
    [SerializeField] private float iFramesTime = 2f;
    private float iFramesCounter = 0f;

    [Header("Dashes")]
    [SerializeField] private int MaxDashes = 3;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float currentDashTime = 0.3f;
    private int dashDirection = 1;
    public int CurrentDashes { get; private set; } = 3;


    private Animator animator;
    private SpriteRenderer ren;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ren = GetComponentInChildren<SpriteRenderer>();
        CurrentDashes = MaxDashes;
    }

    float inputHorizontal = 0;
    float inputVertical = 0;
    bool dashing = false;
    bool damaged = false;
    bool damaging = false;
    float damagedDirection = 0f;

    // Update is called once per frame
    void Update()
    {
        iFramesCounter -= Time.deltaTime;

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        Animate();


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
    bool flipAngle = false;
    bool applyAngle = true;
    void Animate()
    {
        flipAngle = false;
        applyAngle = true;
        ren.flipX = inputHorizontal < 0;

        string targetAnimation = "Idle Fall";
        if (damaging)
        {
            applyAngle = false;
            targetAnimation = "Hurt";
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            damaging = false;
        }
        else if (dashing) targetAnimation = "Dash";
        else if (inputVertical > 0.1f)
        {
            if (Mathf.Abs(inputHorizontal) > 0.1f) targetAnimation = "Float Direction";
            else targetAnimation = "Float";
        }
        else if (inputVertical < -0.1f)
        {
            flipAngle = true;
            targetAnimation = "Dive";
            ren.flipX = false;
        }
        else if (Mathf.Abs(inputHorizontal) > 0.1f) targetAnimation = "Fall Directional";
        else targetAnimation = "Idle Fall";
        
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(targetAnimation)) animator.Play(targetAnimation);
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

        if (-rb.velocity.y >= currentYSpeedCap)
        {
            targetYVelocity = Mathf.Lerp(
                velocityY,
                currentYSpeedCap * Mathf.Sign(rb.velocity.y),
                Time.fixedDeltaTime * YAcceleration);
        }

        //Handle Damage
        if (damaged)
        {
            Debug.LogWarning(damaged);
            damaged = false;
            damaging = true;
            targetYVelocity = damageFlingY;
            targetXVelocity = damageFlingX * damagedDirection;
        }

        //Update Velocity
        rb.velocity = new Vector2(targetXVelocity, targetYVelocity);

        //Update Rotation
        float targetAngle = 180 / (2 * Mathf.PI) * Mathf.Atan(targetXVelocity / targetYVelocity); //includes converstion to degrees
        if (flipAngle) targetAngle = -targetAngle;
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
        if(!applyAngle) transform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleDamager(other);
        HandleEnemy(other);
    }

    void HandleEnemy(Collider2D other)
    {
        if (!dashing) return;
        if (!other.CompareTag("Enemy")) return;

        CurrentDashes++;
    }

    void HandleDamager(Collider2D other)
    {
        if (dashing) return;
        if (iFramesCounter > 0) return;
        if (!other.CompareTag("Damager")) return;

        damaged = true;
        damagedDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
        iFramesCounter = iFramesTime;
    }
}
