using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HorizontalData", menuName = "ScriptableObjects/Enemies/HorizontalData", order = 1)]
public class HorizontalShooterData : ScriptableObject
{
    public EffectData bullet;
    public float bulletSpeed;
    public float bulletInterval;

}
