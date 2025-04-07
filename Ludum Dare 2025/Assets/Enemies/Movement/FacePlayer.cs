using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        Vector3 playerDir = player.transform.position - transform.position;
        EnemyAnimations anim = GetComponent<EnemyAnimations>();
        if (playerDir.x < 0) anim.FlipX(1);
        else anim.FlipX(0);
    }
}
