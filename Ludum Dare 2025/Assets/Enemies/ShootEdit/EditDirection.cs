using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditDirection : ShootEdit
{
    public Vector2 direction;
    public override void doEdit(GameObject bullet)
    {
        float angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
