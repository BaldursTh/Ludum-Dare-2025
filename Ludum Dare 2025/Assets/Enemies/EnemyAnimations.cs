using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator animator;
    public GameObject gunRotater;
    public SpriteRenderer ratRend;
    public SpriteRenderer gunRend;
    GameObject player;
    Rigidbody2D rb;
    bool flipAngle;
    public float shootRecoil;
    public float shootRecoilDistance;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ratRend = GetComponentInChildren<SpriteRenderer>();
        gunRotater = transform.GetChild(0).gameObject.GetChildWithTag("GunPivot");
        player = GameObject.FindGameObjectWithTag("Player");
        if (gunRotater == null) return;
        doShoot = DoShoot();
        gunRend = gunRotater.GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {

    }

    void Animate()
    {

        string targetAnimation = GetAnimation();


        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(targetAnimation)) animator.Play(targetAnimation);
    }

    public void AnimatorShoot() {
        StopCoroutine(doShoot);
        doShoot = DoShoot();
        StartCoroutine(doShoot);
    }
    IEnumerator doShoot;
    bool isShooting = false;
    IEnumerator DoShoot() {
        isShooting = true;
        GameObject child = gunRotater.transform.GetChild(0).gameObject;
        Vector3 start = child.transform.localPosition ;
        Vector3 recoilDir = gunRotater.transform.rotation * gunRotater.transform.up;
        Vector3 end = child.transform.localPosition + (-recoilDir.normalized * shootRecoilDistance);
        while ((child.transform.localPosition - end).magnitude > 0.01f) {
            child.transform.localPosition = Vector3.MoveTowards(child.transform.localPosition, end, shootRecoil);
            yield return null;
        }
        while ((child.transform.localPosition - start).magnitude > 0.01f) {
            child.transform.localPosition = Vector3.MoveTowards(child.transform.localPosition, start, shootRecoil);
            yield return null;
        }
        isShooting = false;
    }

    public void RotateGun(Quaternion newRotation) {
        if (isShooting) return;

        gunRotater.transform.rotation = newRotation;

        if (player == null) return;
        Vector3 playerDir = player.transform.position - transform.position;
        if (playerDir.x < 0) FlipX(1);
        else FlipX(0);
    }

    public void FlipX(int flip) {
        if (ratRend == null) return;
        Vector3 scale = ratRend.transform.parent.localScale;
        if (flip == 2) ratRend.transform.parent.localScale = new Vector3(-scale.x, scale.y, scale.z);
        float newScale = Mathf.Abs(scale.x);
        if (flip == 0) ratRend.transform.parent.localScale = new Vector3(newScale, scale.y, scale.z);
        if (flip == 1) ratRend.transform.parent.localScale = new Vector3(-newScale, scale.y, scale.z);
    }

    public bool isFlipped() {
        if (ratRend == null) return false;
        Vector3 scale = ratRend.transform.parent.localScale;
        return scale.x == -1;
    }

    string GetAnimation()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.01f) {
            return "Move";
        }
        return "Idle";
    }
}
