using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public List<Vector3> movePoints = new List<Vector3>();
    [HideInInspector]
    public int movePointPos;
    public GameObject mover;
    Rigidbody2D rb;
    public virtual void Start() {
        movement = Movement();
        rb = mover.GetComponent<Rigidbody2D>();
        if (movePoints.Count <= 0) AddMovePoints();
        Move();
    }
    public void AddMovePoints() {
        int j = 0;
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (child.tag != "Mover") continue;
            movePoints.Add(child.transform.position);
            if (child.name == mover.name) {
                movePointPos = j;
            }
            j++;
        }

    }
    public void Move() {
        StopCoroutine(movement);
        movement = Movement();
        StartCoroutine(movement);
    }
    IEnumerator movement;
    public virtual IEnumerator Movement() {
        movePointPos = movePointPos.CircleInBounds(movePoints.Count);
        if (movePoints.Count != 0) {
            Vector3 dest = movePoints[movePointPos];
            while (mover.transform.position != dest) {
                mover.transform.position = Vector3.MoveTowards(mover.transform.position, dest, speed);
                yield return null;
            }
        }
        yield return null;
        Move();
    }
}
