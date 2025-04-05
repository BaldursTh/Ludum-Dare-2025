using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemies/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public float hitKnockback;
    public EffectData deathEffect;
}
