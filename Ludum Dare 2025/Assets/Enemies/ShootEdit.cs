using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootEdit : MonoBehaviour
{
    [HideInInspector]
    public HorizontalShooter shooter;
    public abstract void doEdit(GameObject bullet);
}
