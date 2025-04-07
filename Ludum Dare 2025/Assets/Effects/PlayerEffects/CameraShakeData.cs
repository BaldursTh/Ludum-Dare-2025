using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraShakeData", menuName = "ScriptableObjects/CameraEffects/CameraShakeData")]
public class CameraShakeData : ScriptableObject
{
    public float strength;
    public float time;
    public float shakeGap;
    public float rotateToPositionRatio = 0.5f;
}
