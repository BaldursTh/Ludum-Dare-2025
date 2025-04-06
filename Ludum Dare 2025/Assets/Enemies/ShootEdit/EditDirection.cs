using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EditDirection : ShootEdit
{
    public Vector2 direction;
    public override void doEdit(GameObject bullet)
    {
        Vector2 dir = direction;
        if (GetComponent<SpriteRenderer>().flipX) dir = new Vector2(-direction.x, direction.y);
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
