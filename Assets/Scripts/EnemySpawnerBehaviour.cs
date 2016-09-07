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

	public float timer2 = 0;
	public int stage = -1;


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
	int spawnPositionToggleInt= 0;
	void toggleSpawnPosition(int count){
		if (count >= spawnPositionToggleInt) {
			if (spawnPositionint == 1) {
				spawnPositionint = 0;
			} else {
				spawnPositionint = 1;
			}
			spawnPositionToggleInt = -1;
		}
		spawnPositionToggleInt++;


	}

	int spawnPositionint = 0;
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		timer2 += Time.deltaTime;
		while (timer > spawnRate)
        {
            timer -= spawnRate;
		

			if (stage == -1) {
				if (Time.time >= 12f) {
					stage = 0;
					timer2 = 0;
				}
			}
				if (stage == 0) {
					//stage 0
					spawnRate = 1f;

				Spawn (spawnPositions [spawnPositionint], prefabs [0],enemyHitCounter.bulletType.bullet3);
				toggleSpawnPosition (1);
				}

				if (stage == 1) {
				int randomi = Random.Range (0, 2);
				Debug.Log (randomi);
				Spawn (spawnPositions [spawnPositionint], prefabs[randomi], enemyHitCounter.bulletType.bullet3);
				toggleSpawnPosition (1);
						
				}

			if (stage == 2) {
				int randomi = Random.Range (0, 2);
				spawnRate = 0.7f;
				Spawn (spawnPositions [spawnPositionint], prefabs [randomi], enemyHitCounter.bulletType.bullet3);
				toggleSpawnPosition (1);
				if (((int)timer2 % 10) == 0) {
					SpawnOnAll (prefabs [0]);
				}
			}
			if (stage == 3) {
				int randomi = 1;
				spawnRate = 0.5f;
				Spawn (spawnPositions [spawnPositionint], prefabs [randomi], enemyHitCounter.bulletType.bullet3);
				toggleSpawnPosition (2);


			}



			if (timer2 > 25f) {
				timer2 -= 25;
				stage++;

			}

            
        }
	}

    GameObject RandomPrefab()
    {
        if (prefabs.Length > 0) {
            return prefabs[Random.Range(0, prefabs.Length)];
        }
        else {
            return null;
        }
    }

    Transform RandomSpawnPoint()
    {
        if (spawnPositions.Length > 0) {
            return spawnPositions[Random.Range(0, spawnPositions.Length)];
        }
        else {
            return null;
        }
    }

    // Spawns an enemy for each spawn point
    void SpawnOnAll(GameObject prefab)
    {
        foreach (var sp in spawnPositions) {
			Spawn(sp, prefab,enemyHitCounter.bulletType.bullet3);
        }
    }

    // Spawns an enemy to a random spawn point
    void SpawnRandom(GameObject prefab)
    {
		Spawn(RandomSpawnPoint(), prefab,enemyHitCounter.bulletType.bullet3);
    }

	void Spawn(Transform transform, GameObject prefab,enemyHitCounter.bulletType bullet)
    {
        if (transform == null) {
            Debug.LogError("Spawn() called with null transform");
            return;
        }

        if (prefab == null) {
            Debug.LogError("Spawn() called with null prefab");
            return;
        }

        // Add a random vertical offset to the spawn position so that every
        // enemy wont spawn from a same point
        float yOffset = Random.Range(-1.0f, 1.0f) * yRandom;
        Vector3 position = transform.position + new Vector3(0.0f, yOffset, 0.0f);

		GameObject tmp = Instantiate(prefab, position, Quaternion.identity) as GameObject;

		tmp.GetComponent<enemyHitCounter> ().setType (bullet);
    }
}
