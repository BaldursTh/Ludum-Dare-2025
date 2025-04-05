using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    public GameData data;

    private void Start() {
        if (instance != null) {
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
