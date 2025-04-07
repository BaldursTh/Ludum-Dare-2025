using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    [SerializeField] private Transform cam;
    public Camera mainCamera;

    public int Score { get { return (int)Mathf.Floor(-cam.position.y); } }
    public float Distance { get => cam.position.y; }

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
