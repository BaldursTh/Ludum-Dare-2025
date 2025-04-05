using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float Acceleration = 0.1f;
    [SerializeField] private float StartingSpeed = 3f;
    private float speed = 0;

    void Start()
    {
        speed = StartingSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0,-speed*Time.fixedDeltaTime,0);
        speed += Acceleration*Time.fixedDeltaTime;
    }
}
