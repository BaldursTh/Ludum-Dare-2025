using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditDirection : ShootEdit
{
    public Vector2 direction;

    // public void Start() {
    //     Vector2 dir = direction;
    //     EnemyAnimations anim = GetComponent<EnemyAnimations>();
    //     if (GetComponent<EnemyAnimations>().isFlipped()) dir = new Vector2(-direction.x, direction.y);
    //     float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
    //     if (anim.isFlipped()) angle += 180;
    //     anim.RotateGun(Quaternion.AngleAxis(angle, Vector3.forward));
    // }
    public override void doEdit(GameObject bullet)
    {
        EnemyAnimations anim = GetComponent<EnemyAnimations>();
        Vector2 dir = direction;
        if (GetComponent<EnemyAnimations>().isFlipped()) dir = new Vector2(-direction.x, direction.y);
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (anim.isFlipped()) angle += 180;
        anim.RotateGun(Quaternion.AngleAxis(angle, Vector3.forward));
    }
}
