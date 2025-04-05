using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "ScriptableObjects/EffectData", order = 1)]
public class EffectData : ScriptableObject
{
    public GameObject Effect;
    [Min(0)]
    public float startDelay = 0;
    [Header("-1 means infinite duration, can only be positive otherwise")]
    public float duration = -1;
}
