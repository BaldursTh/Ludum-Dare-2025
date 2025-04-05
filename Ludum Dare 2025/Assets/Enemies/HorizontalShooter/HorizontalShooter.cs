using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HorizontalShooter : Enemy
{
    public HorizontalShooterData data;
    public GameObject shootPlace;
    public override void Start()
    {
        base.Start();
        Shoot();
    }
    public void Shoot() {
        GameObject bullet = effectHandler.CreateEffect(data.bullet, shootPlace.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponentInChildren<Rigidbody2D>();
        rb.velocity = data.direction.normalized * data.bulletSpeed;
        float angle = Mathf.Atan2(data.direction.normalized.y, data.direction.normalized.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.Invoke(Shoot, data.bulletInterval);
    }

}
