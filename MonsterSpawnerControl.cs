using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerControl : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] monsters;
    int randomSpawnPoint, randomMonster, randomSpawnPoint2;

    public Transform[] spawnPoints2;
    public GameObject Boss, BossRef;
    public bool bossSpawnAllowed;
    public bool monsterSpawnAllowed;

    void Start()
    {
        monsterSpawnAllowed = true;
        InvokeRepeating("SpawnAMonster", 1f, 1f);

        bossSpawnAllowed = true;
        InvokeRepeating("SpawnABoss", 30f, 30f);
        // StopBirds();
    }

    private void Update()
    {
        StopBirds();
    }

    void SpawnAMonster()
    {
        if (monsterSpawnAllowed == true)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomMonster = Random.Range(0, monsters.Length);
            Instantiate(monsters[randomMonster], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            monsters[randomMonster].GetComponent<MonsterController>().spawnerSignature = randomSpawnPoint;

        }
    }

    void SpawnABoss()
    {
        if (bossSpawnAllowed)
        {
            //  StartCoroutine(StopFuckingBirds());
            //spawnAllowed = false;

            randomSpawnPoint2 = Random.Range(0, spawnPoints2.Length);
            //randomMonster = Random.Range(0, Boss.Leng);
            Instantiate(Boss, spawnPoints2[randomSpawnPoint2].position, Quaternion.Euler(0f, 180f, 0f));
            // Boss[randomMonster].GetComponent<MonsterController>().spawnerSignature = randomSpawnPoint;
            //Again();
            //Boss = GameObject.Find("Boss(Clone)");
        }
    }
    /*
    public void Again()
    {

        if (!Boss.activeInHierarchy)
        {
            spawnAllowed = true;
        }

    }
    */

    /* 
    IEnumerator StopFuckingBirds()
    {
        GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawnerControl>().enabled = false;

        yield return new WaitForSeconds(10);

        GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawnerControl>().enabled = true;
    }
    */

    public void StopBirds()
    {
        BossRef = GameObject.Find("Boss(Clone)");

        if (BossRef)
        {
            monsterSpawnAllowed = false;
        }
        else
        {
            monsterSpawnAllowed = true;
        }
    }
}
