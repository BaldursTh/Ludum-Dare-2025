using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float Acceleration = 0.1f;
    [SerializeField] private float StartingSpeed = 3f;
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

        Debug.Log(player.position.y-transform.position.y);
        if(player.position.y-transform.position.y < -1f)
            targetSpeed = speed+Mathf.Pow(player.position.y-transform.position.y,2);

        transform.position += new Vector3(0,-targetSpeed*Time.fixedDeltaTime,0);
    }
}
