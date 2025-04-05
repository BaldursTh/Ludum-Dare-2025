using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScroller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y-GameManager.instance.Distance<=30f) return;
        transform.position=new Vector3(0,GameManager.instance.Distance,0);
    }
}
