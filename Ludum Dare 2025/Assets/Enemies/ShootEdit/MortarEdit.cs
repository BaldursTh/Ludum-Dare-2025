using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarEdit : ShootEdit
{
    public float gravityScale;
    public HorizontalShooterData data;
    public override void doEdit(GameObject bullet)
    {
        shooter.changeVelocity(getMortarVelocity);
    }

    public Vector2 getMortarVelocity(GameObject bullet) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 origin = bullet.transform.position;
        Vector3 throwPlace = player.transform.position;
        float distance = new Vector2(origin.x - throwPlace.x, origin.z - throwPlace.z).magnitude;
        float bulletS = data.bulletSpeed;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        Vector2 newVelocity = bullet.transform.right * bulletS;
        float time = distance / bulletS;
        float upVelocity = (rb.gravityScale * -9.81f * Mathf.Pow(time, 2) + (origin.y - throwPlace.y)) / -(2 * time);
        newVelocity += (Vector2)bullet.transform.up * upVelocity;
        return newVelocity;
    }
}
