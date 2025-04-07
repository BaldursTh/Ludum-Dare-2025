using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "ScriptableObjects/EnemySpawnerData", order = 1)]
public class EnemyList : ScriptableObject
{
    public List<GameObject> enemies;
    [Range(0, 1)]
    public float noneChance = 0;
    public GameObject getRandomEnemy() {
        if (Random.Range(0f, 1f) < noneChance) return new GameObject();
        return enemies[Random.Range(0, enemies.Count)];
    }
}
