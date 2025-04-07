using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float Acceleration = 0.1f;
    [SerializeField] private float StartingSpeed = 3f;
    [SerializeField] private float DistanceMultiplier = 3f;
    [SerializeField] private float FollowHeight = 2f;
    private float speed = 0;
    public float TargetSpeed { get; private set; }
    private float bonusAcceleration = 1f;
    [SerializeField] private PlayerMovement player;

    void Start()
    {
        speed = StartingSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float targetSpeed = StartingSpeed + DistanceMultiplier * Mathf.Abs(transform.position.y);
        TargetSpeed = targetSpeed;

        if (player.transform.position.y - transform.position.y < FollowHeight)
            targetSpeed += Mathf.Pow(FollowHeight + transform.position.y - player.transform.position.y, System.MathF.E);

        if (player.Dashing)
        {
            TargetSpeed = 0;
            targetSpeed = 0;
            bonusAcceleration = 5f;
        }
        else
        {
            bonusAcceleration = 1;
        }

        speed = Mathf.Lerp(
            speed,
            targetSpeed,
            Time.fixedDeltaTime * Acceleration * bonusAcceleration);

        transform.position += new Vector3(0, -speed * Time.fixedDeltaTime, 0);
    }

    public void StopDash()
    {
        bonusAcceleration = 0f;
    }
}
