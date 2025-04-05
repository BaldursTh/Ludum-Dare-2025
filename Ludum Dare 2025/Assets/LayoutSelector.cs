using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutSelector : MonoBehaviour
{
    public List<LevelDescriptor> spawnableLevels = new();
    public LevelDescriptor beginning;
    public float[] createdLevelHeights = new float[3];
    private float depth;
    private float nextDeleteDepth = 0f;
    private int nextDeleteIndex = 0;

    void Start()
    {
        depth = 0;

        SpawnLevel(beginning);

        SpawnLevel(spawnableLevels[Random.Range(0, spawnableLevels.Count)]);
        SpawnLevel(spawnableLevels[Random.Range(0, spawnableLevels.Count)]);

        nextDeleteDepth = beginning.height;
        nextDeleteIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (-GameManager.instance.Distance <= nextDeleteDepth) return;

        Destroy(transform.GetChild(0).gameObject);
        nextDeleteIndex++;
        if(nextDeleteIndex > 2) nextDeleteIndex = 0;
        nextDeleteDepth += createdLevelHeights[nextDeleteIndex];

        SpawnLevel(spawnableLevels[Random.Range(0, spawnableLevels.Count)]);
    }

    void SpawnLevel(LevelDescriptor level)
    {
        Instantiate(level.scene, new Vector3(0, -depth, 0), Quaternion.identity, transform);
        depth += level.height;

        createdLevelHeights[nextDeleteIndex] = level.height;
        nextDeleteIndex++;
        if(nextDeleteIndex > 2) nextDeleteIndex = 0;
    }
}

[System.Serializable]
public struct LevelDescriptor
{
    public GameObject scene;
    [Min(30)]
    public float height;
}