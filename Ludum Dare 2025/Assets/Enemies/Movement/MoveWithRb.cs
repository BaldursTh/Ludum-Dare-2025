using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithRb : Mover
{
    Rigidbody2D rb;
    public override void Start() {
        rb = mover.GetComponent<Rigidbody2D>();
        base.Start();
    }

    public override IEnumerator Movement() {
        movePointPos = movePointPos.CircleInBounds(movePoints.Count);
        Vector3 dest = movePoints[movePointPos];
        while (mover.transform.position != dest) {
            Vector3 newPosition = Vector3.MoveTowards(mover.transform.position, dest, speed);
            rb.MovePosition(newPosition);
            yield return null;
        }
        rb.velocity = Vector3.zero;
        Move();
    }
}
