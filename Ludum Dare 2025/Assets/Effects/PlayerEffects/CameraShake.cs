using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float startTime;
    float savedTime;
    Vector3 cross = Vector3.zero;
    Vector3 dir = Vector3.zero;
    float screenRatio;
    void Start() {
        screenRatio = Screen.width / Screen.height;
    }
    public void ShakeCamera(CameraShakeData data, Vector3 direction) {
        if (direction != Vector3.zero) direction = Vector3ToScreenVector(direction);
        //print(direction);
        startTime = Time.time;
        savedTime = Time.time;
        cross = Vector3.Cross(transform.forward, direction.normalized);
        dir = direction;

        this.InvokeEveryFrame(() => DoShakeCamera(data, direction), data.time);
        this.Invoke(EndShakeCamera, data.time);
    }

    void DoShakeCamera(CameraShakeData data, Vector3 direction) {
        if (Time.time - savedTime < data.shakeGap) return;
        savedTime = Time.time;
        if (direction == Vector3.zero)  {
            direction = Random.insideUnitSphere;
        }
        //float mult = data.strength * ((Time.time - startTime) / data.time);
        float mult = data.strength * (1 + (Mathf.Abs(transform.InverseTransformDirection(direction).normalized.x) * screenRatio));
        if (transform.position != direction.normalized * mult + transform.position) {
            transform.position = direction.normalized * mult + transform.position;
            //transform.rotation = Quaternion.AngleAxis(mult * data.rotateToPositionRatio, cross.normalized) * transform.parent.transform.rotation;
        }
        else {
            transform.position = -direction.normalized * mult + transform.position;
            //transform.rotation = Quaternion.AngleAxis(mult * data.rotateToPositionRatio, -cross.normalized) * transform.parent.transform.rotation;
        }
    }

    void OnDrawGizmos() {
        float length = 10;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + dir.normalized * length);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + cross.normalized * length);
    }

    void EndShakeCamera() {
        StopAllCoroutines();
        transform.localPosition = Vector3.zero;
        //transform.localRotation = Quaternion.identity;
    }

    public Vector3 Vector3ToScreenVector(Vector3 direction) {
        Vector3 normal = transform.forward;
        return Vector3.ProjectOnPlane(direction, normal);
    }
}
