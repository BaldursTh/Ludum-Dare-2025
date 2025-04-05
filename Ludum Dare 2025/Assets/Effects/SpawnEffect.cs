using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEffect : Effect
{
    public GameObject explosion;
    public override void DoEffect() {
        Instantiate(explosion, this.transform);
    }


}
