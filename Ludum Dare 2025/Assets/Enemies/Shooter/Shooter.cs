using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Shooter : Enemy
{
    public ShooterData data;
    public GameObject shootPlace;
    public List<ShootEdit> edits = new List<ShootEdit>();
    public List<GameObject> bullets = new List<GameObject>();
    public delegate Vector2 GetVelocity(GameObject bullet);
    GetVelocity getVelocity;
    public override void Start()
    {
        base.Start();
        edits = GetComponents<ShootEdit>().ToList();
        edits.ForEach(edit => edit.shooter = this);
        Shoot();
    }
    public void Shoot() {
        bullets = new List<GameObject>();
        GameObject newBullet = addBullet();
        getVelocity += defaultGetVelocity;
        foreach(ShootEdit edit in edits) {
            edit.doEdit(newBullet);
        }
        bullets.ForEach(bullet => {
            Rigidbody2D rb = bullet.GetComponentInChildren<Rigidbody2D>();
            rb.velocity = getVelocity(bullet);
        });
        this.Invoke(Shoot, data.bulletInterval);
    }

    public GameObject addBullet() {
        GameObject bullet = effectHandler.CreateEffect(data.bullet, shootPlace.transform.position, Quaternion.identity);
        bullets.Add(bullet);
        return bullet;
    }

    public void changeVelocity(GetVelocity vel) {
        foreach(Delegate d in getVelocity.GetInvocationList())
        {
            getVelocity -= (GetVelocity)d;
        }
        getVelocity += vel;
    }
    public Vector2 defaultGetVelocity(GameObject bullet) {
        return bullet.transform.right * data.bulletSpeed;
    }
}
