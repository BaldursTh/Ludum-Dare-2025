using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceOnShot : ShootEdit
{
    public float shotForce;
    public override void doEdit(GameObject bullet)
    {
        Vector2 direction = GetComponent<EnemyAnimations>().gunRotater.transform.rotation * -Vector3.right;
        if (GetComponent<EnemyAnimations>().isFlipped()) direction *= -1;
        GetComponent<Rigidbody2D>().AddForce(shotForce * direction.normalized);
    }
}
