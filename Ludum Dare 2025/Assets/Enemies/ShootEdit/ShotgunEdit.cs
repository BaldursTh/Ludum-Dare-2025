using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEdit : ShootEdit
{
    [Min(2)]
    public int bulletCount;
    [Min(1)]
    public float spreadAngle;
    public override void doEdit(GameObject bullet)
    {
        Quaternion initial = bullet.transform.rotation;
        float spreadInterval = spreadAngle / (bulletCount - 1);
        float startSpread = spreadAngle / 2;
        GameObject temp = bullet;
        for (int i = 0; i < bulletCount; i++) {
            if (i != 0) temp = shooter.addBullet();
            Quaternion newRotation = initial * Quaternion.Euler(Vector3.forward * (startSpread - (i * spreadInterval)));
            temp.transform.rotation = newRotation;
        }
    }
}
