using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : Enemy
{
    public ShooterData data;
    public GameObject shootPlace;
    public List<ShootEdit> edits = new List<ShootEdit>();
    public List<GameObject> bullets = new List<GameObject>();
    public delegate Vector2 GetVelocity(GameObject bullet);
    GetVelocity getVelocity;
    public override void Awake()
    {
        base.Awake();
        edits = GetComponents<ShootEdit>().ToList();
        edits.ForEach(edit => edit.shooter = this);
        started = false;
    }
    bool started = false;
    public override void Update()
    {
        base.Update();
        if (started) return;
        if (transform.position.y + transform.lossyScale.y > Camera.main.transform.position.y - Camera.main.orthographicSize) {
            started = true;
            ShootLoop();
        }
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
        GetComponent<EnemyAnimations>().AnimatorShoot();
        GetComponentInChildren<AudioSource>().Play();
    }

    public void ShootLoop() {
        Shoot();
        this.Invoke(ShootLoop, data.bulletInterval);
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
