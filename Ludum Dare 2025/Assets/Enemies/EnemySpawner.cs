using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyList enemyList;
    public void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy() {
        Instantiate(enemyList.getRandomEnemy(), transform.position, Quaternion.identity);
    }
}
