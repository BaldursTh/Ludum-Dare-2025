using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator animator;
    public GameObject gunRotater;
    public SpriteRenderer ratRend;
    public SpriteRenderer gunRend;
    Rigidbody2D rb;
    bool flipAngle;
    public float shootRecoil;
    public float shootRecoilDistance;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ratRend = GetComponentInChildren<SpriteRenderer>();
        gunRotater = gameObject.GetChildWithTag("GunRotater");
        if (gunRotater == null) return;
        doShoot = DoShoot();
        gunRend = gunRotater.GetComponentInChildren<SpriteRenderer>();
    }

    void Animate()
    {
        flipAngle = false;

        string targetAnimation = GetAnimation();

        ratRend.flipX = !flipAngle;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(targetAnimation)) animator.Play(targetAnimation);
    }

    public void AnimatorShoot() {
        StopCoroutine(doShoot);
        StartCoroutine(doShoot);
    }
    IEnumerator doShoot;
    bool isShooting = false;
    IEnumerator DoShoot() {
        isShooting = true;
        GameObject child = gunRotater.transform.GetChild(0).gameObject;
        Vector3 start = child.transform.position;
        Vector3 end = child.transform.position + (-gunRotater.transform.right * shootRecoilDistance);
        while ((child.transform.position - end).magnitude > 0.01f) {
            Vector3 newPosition = Vector3.MoveTowards(child.transform.position, end, shootRecoil);
            rb.MovePosition(newPosition);
            yield return null;
        }
        while ((child.transform.position - start).magnitude > 0.01f) {
            Vector3 newPosition = Vector3.MoveTowards(child.transform.position, start, shootRecoil);
            rb.MovePosition(newPosition);
            yield return null;
        }
        isShooting = false;
    }

    public void RotateGun(Quaternion rotation) {
        if (isShooting) return;
        gunRotater.transform.rotation = rotation;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        Vector3 playerDir = player.transform.position - transform.position;
        if (playerDir.x < 0) gunRend.flipY = true;
        else gunRend.flipY = false;
    }

    string GetAnimation()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.01f) {
            if (rb.velocity.x < 0) flipAngle = true;
            return "Move";
        }
        return "Idle";
    }
}
