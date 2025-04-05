using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootEdit : MonoBehaviour
{
    [HideInInspector]
    public Shooter shooter;
    public abstract void doEdit(GameObject bullet);
}
