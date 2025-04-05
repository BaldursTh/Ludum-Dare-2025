using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "ScriptableObjects/EnemySpawnerData", order = 1)]
public class EnemyList : ScriptableObject
{
    public List<GameObject> enemies;

    public GameObject getRandomEnemy() {
        return enemies[Random.Range(0, enemies.Count)];
    }
}
