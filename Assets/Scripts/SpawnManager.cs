using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public GameObject prefabToSpawn;
    public Transform spawnArea;
    public int numberOfPrefabsToSpawn;

    public void CreateEnemies(Transform player)
    {
        GameObject modifiedPrefab = prefabToSpawn;
        AIDestinationSetter ai = modifiedPrefab.GetComponentInChildren<AIDestinationSetter>();
        ai.target = player;
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            // Generate a random position within the spawn area
            Vector3 randomPosition = GetRandomPosition();

            // Instantiate the prefab at the random position
            Instantiate(modifiedPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Get a random point within the spawn area
        Vector3 randomPoint = new Vector3(
            Random.Range(spawnArea.position.x - spawnArea.localScale.x * 50f, spawnArea.position.x + spawnArea.localScale.x * 50f),
            spawnArea.position.y,
            Random.Range(spawnArea.position.z - spawnArea.localScale.z * 50f, spawnArea.position.z + spawnArea.localScale.z * 50f)
        );
        Debug.Log(spawnArea);
        Debug.Log(randomPoint);
        return randomPoint;
    }
}
