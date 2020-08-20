using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    /*
    public GameObject[] BossPositions;
    public int num = 0;
    public float minDistance;
    public float Speed;

    public bool rand = false;

    public bool go = true;

    public GameObject Spawner2;

    // Start is called before the first frame update
    void Start()
    {
        Spawner2 = GameObject.Find("MonsterSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, BossPositions[num].transform.position);

        if (go)
        {
            if (distance > minDistance)
            {
                Move();
            }
            else
            {
                if (!rand)
                {
                    if (num + 1 == BossPositions.Length)
                    {
                        num = 0;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num = Random.Range(0, BossPositions.Length);
                }
            }
        }
    }

    public void Move()
    {
        // BossPositions[num].transform.position);
        // gameObject.transform.Translate(BossPositions[num].transform.position);
        //  gameObject.transform.LookAt(BossPositions[num].transform.position);
        gameObject.transform.position = BossPositions[num].transform.position;
        gameObject.transform.position += gameObject.transform.forward * Speed * Time.deltaTime;
          
        if (transform.position == )
        {
            gameObject.transform.position += gameObject.transform.forward * Speed * Time.deltaTime;
        }
        else if()
        {
            Sideways();
        }
          
    }*/

    [SerializeField]
    float moveSpeed = 2f;
    int waypointIndex = 7;
    float maxHealth;
    
    public Transform[] waypoints;
    public Slider bossHealth;

    void Start()
    {
        maxHealth = GetComponent<Target>().health;
        for(int childIndex = 0; childIndex < waypointIndex; childIndex++)
        {
            waypoints[childIndex] = GameObject.Find("BossPath").transform.GetChild(childIndex).transform;
        }
        waypointIndex = 0;
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        MoveBoss();
        HealthUpdate();
    }

    void MoveBoss()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;
    }    
    void HealthUpdate()
    {
        float health = GetComponent<Target>().health;
        bossHealth.value = health / maxHealth;
        Debug.Log(health + "/" + maxHealth);
    }
}
