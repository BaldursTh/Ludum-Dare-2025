using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    public Camera mainCamera;
    [SerializeField] public CameraMovement cam;

    public int Score { get { return (int)Mathf.Floor(-cam.transform.position.y); } }
    public float Distance { get => cam.transform.position.y; }

    private void Start()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mainCamera = cam.GetComponentInChildren<Camera>();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
