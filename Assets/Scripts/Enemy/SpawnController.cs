using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

	public Transform[] spawnPoints;
	public GameObject[] enemyPrefabs;

	int wave = -1;
	float nextWave = 0f;

	float[][] waves = new float[][]{
		new float[]{1.8f, 0.5f, 0.2f, 0.0f}, // 4 16
		new float[]{1.5f, 1.5f, 0.0f, 0.1f}, // 
		new float[]{0.8f, 2.5f, 0.0f, 0.2f},
		new float[]{2.9f, 1.5f, 0.2f, 0.2f},
		new float[]{1.9f, 2.5f, 0.1f, 0.0f},

		new float[]{2.9f, 2.1f, 0.3f, 0.3f},
		new float[]{0.9f, 4.5f, 0.4f, 0.0f},
		new float[]{5.9f, 0.5f, 0.0f, 0.5f},
		new float[]{3.2f, 3.5f, 0.0f, 0.0f}, // 24 52
		new float[]{0.4f, 0.4f, 1.0f, 1.0f},
	};

	void Start ()
	{
		nextWave = Time.timeSinceLevelLoad + 5f;
	}

	void Update ()
	{
		if (Time.timeSinceLevelLoad >= nextWave) {
			wave++;
			nextWave += 5f;

			StartWave ();
		}
	}

	void StartWave ()
	{
		int waveNumber = wave % 10;
		int waveMultiplier = ((int) System.Math.Floor(wave / 10f)) + 1;

		float[] waveLine = waves [waveNumber];

		for (int type = 0; type < waveLine.Length; ++type) { // Loop all enemy types
			GameObject enemyPrefab = enemyPrefabs [type];
			int enemyTypeCount = (int) System.Math.Floor(waveLine[type] * waveMultiplier) * spawnPoints.Length;
			for (int i = 0; i < enemyTypeCount; i++) {
				SpawnEnemy (enemyPrefab);
			}
		}
	}

	void SpawnEnemy (GameObject enemyPrefab)
	{
		Transform spawnPoint = GetNextSpawnPoint ();

		Instantiate (enemyPrefab, spawnPoint.position, Quaternion.identity);
	}

	Transform GetNextSpawnPoint ()
	{
		return spawnPoints [0];
	}
}
