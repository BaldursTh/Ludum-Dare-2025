using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarEdit : ShootEdit
{
    public float gravityScale;
    ShooterData data;
    GameObject player;
    EnemyAnimations anim;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<EnemyAnimations>();
        data = shooter.data;
    }
    public override void doEdit(GameObject bullet)
    {
        shooter.changeVelocity(getMortarVelocity);
    }
    void Update()
    {
        GameObject bullet = transform.GetChild(0).gameObject;
        Vector2 newVelocity  = getMortarVelocity(bullet);
        float angle = Mathf.Atan2(newVelocity.y, newVelocity.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (anim.isFlipped()) newRotation *= Quaternion.AngleAxis(180, Vector3.forward);
        GetComponent<EnemyAnimations>().RotateGun(newRotation);
    }

    public Vector2 getMortarVelocity(GameObject bullet) {
        Vector3 origin = bullet.transform.position;
        Vector3 throwPlace = player.transform.position;
        float distance = new Vector2(origin.x - throwPlace.x, origin.z - throwPlace.z).magnitude;
        float bulletS = data.bulletSpeed;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = gravityScale;
        Vector2 newVelocity = bullet.transform.right * bulletS;
        float time = distance / bulletS;
        float upVelocity = (gravityScale * -9.81f * Mathf.Pow(time, 2) + (origin.y - throwPlace.y)) / -(2 * time);
        newVelocity += (Vector2)bullet.transform.up * upVelocity;
        if (anim.isFlipped()) newVelocity *= new Vector2(-1, 1);
        return newVelocity;
    }

}
