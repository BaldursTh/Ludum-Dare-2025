using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D) {
        collision2D.transform.SetParent(transform);
    }
    void OnCollisionExit2D(Collision2D collision2D) {
        collision2D.transform.SetParent(null);
    }
}
