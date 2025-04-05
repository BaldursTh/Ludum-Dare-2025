using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static IEnumerator Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        IEnumerator routine = InvokeRoutine(f, delay);
        mb.StartCoroutine(routine);
        return routine;
    }

    private static IEnumerator InvokeRoutine(Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }

    public static float FindDistanceTo(this Vector3 start, Vector3 end) {
        return Mathf.Abs((start - end).magnitude);
    }

    public static IEnumerator InvokeEveryFrame(this MonoBehaviour mb, Action f, float duration) {
        IEnumerator routine = InvokeRoutineEveryFrame(f);
        mb.StartCoroutine(routine);
        Invoke(mb, () => {
            if (routine != null) mb.StopCoroutine(routine);
        }, duration);
        return routine;
    }

    private static IEnumerator InvokeRoutineEveryFrame(Action f) {
        while (true) {
            f();
            yield return null;
        }
    }

    public static Vector3 Rotate(this Vector3 vector, float angle) {
        return Quaternion.AngleAxis(angle, Vector3.up) * vector;
    }

    public static Vector3 Flatten(this Vector3 vector) {
        return new Vector3(vector.x, 0, vector.z);
    }

    public static bool IsInRange(this MonoBehaviour mb, GameObject objectToCheck, float range) {
        float dist = Mathf.Abs((mb.transform.position - objectToCheck.transform.position).magnitude);
        if (dist > range) return false;
        return true;
    }
    public static int CircleInBounds(this int adder, int bound) {
        adder += 1;
        if (adder >= bound) adder = 0;
        return adder;
    }
}
