using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    public static GameManager instance;
    public GameData data;
    [SerializeField] private Transform cam;

    private void Start() {
        if (instance != null) {
    public int Score { get { return (int)Mathf.Floor(-cam.position.y); } }
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

//     public int getScore() {
//         return
//     }
}
