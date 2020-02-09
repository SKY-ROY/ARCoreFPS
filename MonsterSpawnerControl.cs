using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerControl : MonoBehaviour
{
	public Transform[] spawnPoints;
	public GameObject[] monsters;
	int randomSpawnPoint, randomMonster;
	//public static bool spawnAllowed;
	public bool spawnAllowed;
	// Use this for initialization
	void Start()
	{
		spawnAllowed = true;
		InvokeRepeating("SpawnAMonster", 2f, 2f);
	}

	void SpawnAMonster()
	{
		if (spawnAllowed)
		{
			randomSpawnPoint = Random.Range(0, spawnPoints.Length);
			randomMonster = Random.Range(0, monsters.Length);
			Instantiate(monsters[randomMonster], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
			monsters[randomMonster].GetComponent<MonsterController>().spawnerSignature = randomSpawnPoint;
		}
	}
}
