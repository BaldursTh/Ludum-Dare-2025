using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [HideInInspector]
    public float duration = -1;
    [HideInInspector]
    public float delay = 0;
    public virtual void Start() {
        if (duration == -1) duration = Mathf.Infinity;
        if (delay < 0) delay = 0;
        this.Invoke(DoEffect, delay);
        if (duration != 0) this.Invoke(DestroyEffect, duration + delay);
    }
    public virtual void DoEffect() {

    }

    public void DestroyEffect() {
        gameObject.transform.SetParent(null, true);
        ParticleSystem[] pss = gameObject.GetComponentsInChildren<ParticleSystem>();
        float highestLifetime = 0;
        foreach (ParticleSystem ps in pss) {
            var emission = ps.emission;
            emission.rateOverDistance = 0;
            emission.rateOverTime = 0;
            emission.enabled = false;
            var main = ps.main;
            if (main.startLifetime.constantMax > highestLifetime) highestLifetime = main.startLifetime.constantMax;
        }
        TrailRenderer[] trails = gameObject.GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer trail in trails) {
            if (trail.time > highestLifetime) highestLifetime = trail.time;
        }
        gameObject.transform.localScale /= gameObject.transform.localScale.x;
        Destroy(gameObject, highestLifetime);
    }
}
