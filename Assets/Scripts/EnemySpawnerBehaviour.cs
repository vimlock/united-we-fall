using UnityEngine;
using System.Collections;

public class EnemySpawnerBehaviour : MonoBehaviour {

    // List of enemy object prefabs we can choose from
    // Every time a new enemy is created, a prefab is randomly selected from these
    // and instanciated.
    public GameObject [] prefabs;

    // How many enemies per second are spawned.
    public float spawnRate = 1.0f;

    // Internal timer which tracks time between enemy spawns.
    private float timer = 0.0f;

    // List of positions from which the enemies will spawn.
    public Transform [] spawnPositions;

    // Maximum random y offset to the enemy spawn positions
    public float yRandom;

	// Use this for initialization
	void Start () {
        if (prefabs.Length == 0)
        {
            Debug.LogError("No prefabs assigned");
        }

        if (spawnPositions.Length == 0)
        {
            Debug.LogError("No spawn positions assigned");
        }

        foreach (var p in prefabs)
        {
            if (p == null) {
                Debug.LogError("Unassigned prefab");
            }
        }

        foreach (var i in spawnPositions)
        {
            if (i == null)
            {
                Debug.LogError("Unassigned transform");
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        while (timer > spawnRate)
        {
            timer -= spawnRate;
            Spawn();
        }
	}

    void Spawn()
    {
        // no prefabs
        if (spawnPositions.Length == 0 || prefabs.Length == 0)
        {
            return;
        }

        Transform transform = spawnPositions[Random.Range(0, spawnPositions.Length)];
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // unassigned prefabs?
        if (prefab == null || transform == null)
        {
            return;
        }

        float yOffset = Random.Range(-1.0f, 1.0f) * yRandom;
        Vector3 position = transform.position + new Vector3(0.0f, yOffset, 0.0f);

        GameObject.Instantiate(prefab, position, Quaternion.identity);
    }
}
