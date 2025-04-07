using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutSelector : MonoBehaviour
{
    public List<LevelDescriptor> spawnableLevels = new();
    public LevelDescriptor beginning;
    public float[] createdLevelDepths = new float[3];
    private float depth;
    private int nextDeleteIndex = 0;

    void Start()
    {
        depth = 0;

        SpawnLevel(beginning);

        SpawnLevel(spawnableLevels[Random.Range(0, spawnableLevels.Count)]);
        SpawnLevel(spawnableLevels[Random.Range(0, spawnableLevels.Count)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (-GameManager.instance.Distance <= createdLevelDepths[nextDeleteIndex] + Camera.main.orthographicSize) return;

        Destroy(transform.GetChild(0).gameObject);

        SpawnLevel(spawnableLevels[Random.Range(0, spawnableLevels.Count)]);
    }

    private void IncrementIndex()
    {
        nextDeleteIndex++;
        if (nextDeleteIndex > 2) nextDeleteIndex = 0;
    }

    void SpawnLevel(LevelDescriptor level)
    {
        Instantiate(level.scene, new Vector3(0, -depth, 0), Quaternion.identity, transform);
        depth += level.height;

        createdLevelDepths[nextDeleteIndex] = depth;

        IncrementIndex();
    }
}

[System.Serializable]
public struct LevelDescriptor
{
    public GameObject scene;
    [Min(30)]
    public float height;
}