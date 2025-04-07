using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HomeIn : ShootEdit
{
    GameObject player;
    EnemyAnimations anim;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<EnemyAnimations>();
    }
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        if (anim.isFlipped()) angle += 180;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GetComponent<EnemyAnimations>().RotateGun(newRotation);
    }
    public override void doEdit(GameObject bullet)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.transform.rotation = newRotation;
        // GetComponent<EnemyAnimations>().RotateGun(newRotation);
    }
}
