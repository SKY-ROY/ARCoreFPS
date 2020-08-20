using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomSpawner : MonoBehaviour
{


    public Transform[] spawnPoints;
    public GameObject[] Spit;
    public bool spawnAllowed;
    int randomSpawnPoint, spitit;
    void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpitUp", 3f, 3f);
    }

    void SpitUp()
    {
        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            spitit = Random.Range(0, Spit.Length);
            Instantiate(Spit[spitit], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            // Boss[randomMonster].GetComponent<MonsterController>().spawnerSignature = randomSpawnPoint;
        }
    }
}
