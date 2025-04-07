using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public bool Attacking = false;
    [SerializeField] private Rigidbody2D rb;
    [Header("Player Stats")]
    [SerializeField] private float XSpeed = 5f;
    [SerializeField] private float XAcceleration = 3f;
    [SerializeField] private float attackSpeedThreshold = 10;

    [Header("Vertical Movement")]
    //[SerializeField] private float YSpeedCap = 3.7f;
    [SerializeField] private float YFallSpeedCap = 3.3f;
    [SerializeField] private float YFloatSpeedCap = -1.1f;
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
    private int dashDirectionX = 1;
    private int dashDirectionY = -1;
    public int CurrentDashes { get; private set; } = 3;


    private Animator animator;
    private SpriteRenderer ren;
    public GameObject rotatePoint;
    [SerializeField] private GameObject deathScreen;
    private EffectHandler effectHandler;
    [SerializeField] private EffectData deathEffect;
    [SerializeField] private EffectData stompEffect;
    [SerializeField] private EffectData stompDamager;
    [SerializeField] private ParticleSystem descentEffect;
    [SerializeField] private CameraShakeData camShakeDataOnSlam;

    [SerializeField] private AudioSource flingSFX, slamSFX, landSFX, hitSFX, dashSFX, diveSFX, diveLoopSFX, killSFX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        ren = GetComponentInChildren<SpriteRenderer>();
        effectHandler = gameObject.AddComponent<EffectHandler>();
        if (deathScreen == null) deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        deathScreen.SetActive(false);
        CurrentDashes = MaxDashes;
        Time.timeScale = 1;
        InitDescentEffect();
        StopDescentEffect();
        baseThreshold = attackSpeedThreshold;
    }

    float inputHorizontal = 0;
    float inputVertical = 0;
    public bool Dashing { get; private set; } = false;
    bool damaged = false;
    bool damaging = false;
    [SerializeField] bool floored = false;
    float damagedDirection = 0f;
    float baseThreshold;

    // Update is called once per frame
    void Update()
    {
        iFramesCounter -= Time.deltaTime;

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        Animate();

        if (Dashing) return;

        if (inputHorizontal <= -0.1f) dashDirectionX = -1;
        else if (inputHorizontal >= 0.1f) dashDirectionX = 1;
        else dashDirectionX = 0;

        if (inputVertical <= -0.1f) dashDirectionY = -1;
        // else if (inputVertical >= 0.1f) dashDirectionY = 1;
        else dashDirectionY = 0;

        if (Input.GetButtonDown("Jump") && CurrentDashes > 0)
        {
            StartDashEffect();
            CurrentDashes--;
            Dashing = true;
            currentDashTime = dashTime;
            dashSFX.Play();
        }

        DeathCheck();
        AttackCheck();
    }
    bool flipAngle = false;
    bool applyAngle = true;
    void Animate()
    {
        flipAngle = false;
        applyAngle = true;
        ren.flipX = inputHorizontal < 0;

        string targetAnimation = GetAnimation();
        // if (damaging)
        //     Debug.Log(damaging.ToString() + targetAnimation);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(targetAnimation)) animator.Play(targetAnimation);
    }

    string GetAnimation()
    {
        StopDescentEffect();
        if (damaging)
        {
            applyAngle = false;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                damaging = false;
            return "Hurt";
        }
        else if (Dashing) return "Dash";
        else if (floored)
        {
            if (Mathf.Abs(inputHorizontal) > 0.1f) return "Walk";
            return "Idle";
        }
        else if (inputVertical > 0.1f)
        {
            if (Mathf.Abs(inputHorizontal) > 0.1f) return "Float Direction";
            else return "Float";
        }
        else if (inputVertical < -0.1f)
        {
            DescentEffect();
            flipAngle = true;
            ren.flipX = false;
            return "Dive";
        }
        else if (Mathf.Abs(inputHorizontal) > 0.1f) return "Fall Directional";
        else return "Idle Fall";
    }

    void FixedUpdate()
    {
        //Handle Horizontal Movement
        float targetAcceleration = XAcceleration;
        attackSpeedThreshold = GameManager.instance.cam.TargetSpeed + baseThreshold;

        float targetXVelocity = inputHorizontal * XSpeed;

        float velocityX = rb.velocity.x;
        targetXVelocity = Mathf.Lerp(
            velocityX,
            targetXVelocity,
            Time.fixedDeltaTime * XAcceleration);

        float velocityY = rb.velocity.y;
        float targetYVelocity = 0f;
        //Handle Dash
        if (Dashing)
        {
            targetXVelocity = dashSpeed * dashDirectionX;

            currentDashTime -= Time.fixedDeltaTime;
            targetYVelocity = dashDirectionY * dashSpeed;
            if (currentDashTime < 0f)
            {
                StopDashEffect();
                Dashing = false;
            }
        }
        else
        {
            //Handle Vertical Movement
            float currentYSpeedCap = GameManager.instance.cam.TargetSpeed;
            targetYVelocity = velocityY + Time.fixedDeltaTime * Gravity;

            if (inputVertical <= -0.1f) currentYSpeedCap = YFallSpeedCap + currentYSpeedCap;
            if (inputVertical >= 0.1f) currentYSpeedCap = YFloatSpeedCap + currentYSpeedCap;

            if (-rb.velocity.y >= currentYSpeedCap)
            {
                targetYVelocity = Mathf.Lerp(
                    velocityY,
                    currentYSpeedCap * Mathf.Sign(rb.velocity.y),
                    Time.fixedDeltaTime * YAcceleration);
            }
        }


        //Handle Damage
        if (damaged)
        {
            //Debug.LogWarning(damaged);
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
        if (!applyAngle || floored)
        {
            rotatePoint.transform.rotation = Quaternion.identity;
        }
        else
        {
            if (float.IsFinite(targetAngle))
                rotatePoint.transform.eulerAngles = new Vector3(0, 0, targetAngle);
        }
    }
    public bool getDash()
    {
        return Dashing;
    }
    public void AddDash()
    {
        killSFX.Play();

        if (CurrentDashes >= MaxDashes) return;
        CurrentDashes++;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        HandleDamager(other);
        HandleEnemy(other);
        HandleFloor(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("World")) return;
        floored = false;
    }

    void HandleFloor(Collider2D other)
    {
        if (!other.CompareTag("World")) return;

        floored = true;

        if (!Attacking)
        {
            landSFX.Play();
            return;
        }
        ;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, 1 << 0);
        effectHandler.CreateEffect(stompEffect, hit.point, Quaternion.identity);
        effectHandler.CreateEffect(stompDamager, hit.point, Quaternion.identity);
        Camera.main.transform.parent.GetComponent<CameraShake>().ShakeCamera(camShakeDataOnSlam, Vector2.down);
        slamSFX.Play();
    }

    void HandleDamager(Collider2D other)
    {
        if (!other.CompareTag("Damager")) return;
        TakeDamage(other);
    }

    void HandleEnemy(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (Dashing)
        {
            // AddDash();
            return;
        }
        if (Attacking)
        {
            // AddDash();
            return;
        }
        TakeDamage(other);
    }

    void TakeDamage(Collider2D other)
    {
        if (Dashing) return;
        if (iFramesCounter > 0) return;

        hitSFX.Play();
        flingSFX.Play();
        damaged = true;
        damagedDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
        iFramesCounter = iFramesTime;
    }

    void DeathCheck()
    {
        float threshold = Camera.main.transform.position.y + Camera.main.orthographicSize + transform.lossyScale.y;
        if (transform.position.y > threshold) Death();
    }
    void AttackCheck()
    {
        if (Mathf.Abs(rb.velocity.y) >= attackSpeedThreshold)
        {
            Attacking = true;
            if (!diveSFX.isPlaying && !diveLoopSFX.isPlaying) diveSFX.Play();
            if (diveSFX.isPlaying && diveSFX.time > diveSFX.clip.length - 0.1f) diveLoopSFX.Play();
            return;
        }
        Attacking = false;
        diveSFX.Stop();
        diveLoopSFX.Stop();

    }
    float defaultRate;
    float defaultLifetime;
    void InitDescentEffect()
    {
        var emission = descentEffect.emission;
        defaultRate = emission.rateOverTimeMultiplier;
        var main = descentEffect.main;
        defaultLifetime = main.startLifetimeMultiplier;
    }
    void DescentEffect()
    {
        float rate = Mathf.Clamp(attackSpeedThreshold - Mathf.Abs(rb.velocity.y), 1f, Mathf.Infinity);
        var emission = descentEffect.emission;
        emission.rateOverTimeMultiplier = defaultRate / Mathf.Sqrt(rate);
        var main = descentEffect.main;
        main.startLifetimeMultiplier = defaultLifetime / Mathf.Sqrt(rate);
        if (Attacking) main.startColor = Color.red;
        else main.startColor = Color.white;
    }

    void StopDescentEffect()
    {
        var emission = descentEffect.emission;
        emission.rateOverTimeMultiplier = 0;
    }

    void Death()
    {
        deathScreen.SetActive(true);
        gameObject.SetActive(false);
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        effectHandler.CreateEffect(deathEffect, transform.position, Quaternion.identity);
    }

    [SerializeField] ParticleSystem dash;
    void StartDashEffect()
    {
        int scale = dashDirectionX != 0 ? dashDirectionX : 1;
        dash.transform.localScale = new Vector3(scale, 1, 1);
        dash.Play();
    }

    void StopDashEffect()
    {
        dash.Stop();
        GameManager.instance.cam.StopDash();
    }
}
