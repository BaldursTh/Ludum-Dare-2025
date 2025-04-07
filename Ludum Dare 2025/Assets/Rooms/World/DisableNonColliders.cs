using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisableNonColliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PlatformEffector2D>() == null) transform.GetChild(i).GetComponent<TilemapRenderer>().enabled = false;
        }
    }
}
