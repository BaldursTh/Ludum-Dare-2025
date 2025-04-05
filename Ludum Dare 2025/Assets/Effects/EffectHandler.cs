using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    public List<GameObject> activeEffects = new List<GameObject>();
    public GameObject CreateEffect(EffectData data, Vector3 position, Quaternion rotation) {
        return DoCreateEffect(data, null, position, rotation);
    }
    public GameObject CreateEffect(EffectData data, Transform transform) {
        return DoCreateEffect(data, transform, Vector3.zero, Quaternion.identity);
    }

    public GameObject DoCreateEffect(EffectData data, Transform transform, Vector3 position, Quaternion rotation) {
        GameObject effectCopy;
        if (data.Effect.GetComponent<Effect>() == null) {
            effectCopy = new GameObject("Effect Spawner", typeof(SpawnEffect));
            effectCopy.GetComponent<SpawnEffect>().explosion = data.Effect;
        }
        else effectCopy = data.Effect;

        GameObject createdEffect;
        if (transform != null) createdEffect = Instantiate(effectCopy, transform);
        else createdEffect = Instantiate(effectCopy, position, rotation);

        createdEffect.GetComponent<Effect>().duration = data.duration;
        createdEffect.GetComponent<Effect>().delay = data.startDelay;

        if (data.Effect.GetComponent<Effect>() == null) Destroy(effectCopy);
        activeEffects.Add(createdEffect);
        return createdEffect;
    }

    public void DestroyEffect(GameObject effect) {
        activeEffects.Remove(effect);
        if (effect == null) return;
        if (effect.GetComponent<Effect>()) effect.GetComponent<Effect>().DestroyEffect();
        else Destroy(effect);
    }
    public void DestroyAllEffects() {
        List<GameObject> copy = new List<GameObject>(activeEffects);
        foreach(GameObject effect in copy) {
            DestroyEffect(effect);
        }
        activeEffects.Clear();
    }
}
