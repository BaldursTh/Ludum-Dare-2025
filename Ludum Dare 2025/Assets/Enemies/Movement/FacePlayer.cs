using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerDir = player.transform.position - transform.position;
        if (playerDir.x < 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;
    }
}
