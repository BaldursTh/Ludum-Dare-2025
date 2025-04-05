using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float Acceleration = 0.1f;
    [SerializeField] private float StartingSpeed = 3f;
    [SerializeField] private float FollowHeight = 2f;
    private float speed = 0;
    [SerializeField] private Transform player;

    void Start()
    {
        speed = StartingSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = StartingSpeed+Acceleration*Mathf.Abs(transform.position.y);

        float targetSpeed = speed;
        
        if(player.position.y-transform.position.y < FollowHeight)
            targetSpeed = speed+Mathf.Pow(FollowHeight+transform.position.y-player.position.y,System.MathF.E);

        transform.position += new Vector3(0,-targetSpeed*Time.fixedDeltaTime,0);
    }
}
