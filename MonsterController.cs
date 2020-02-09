using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
	private Rigidbody rb;
	private GameObject target;
	private float moveSpeed;
	
	public int targetCount;
	public GameObject[] targetPoints;

	public float rotFrequency = 50f;
	public int spawnerSignature;
	public int hitDamage = 1;
	public char creatureType = 'P';

	Vector3 directionToTarget;

	void Start()
	{
		targetCount = GameObject.Find("TargetPoints").transform.childCount;

		targetPoints = new GameObject[targetCount];
		for (int i = 0; i < targetCount; i++)
		{
			targetPoints[i] = GameObject.Find("TargetPoints").transform.GetChild(i).gameObject;
		}

		rb = GetComponent<Rigidbody>();
		moveSpeed = Random.Range(1f, 5f);
	}
	void Update()
	{
		FollowTarget(spawnerSignature);
		PositionalDestroyer();
	}
	private void OnCollisionEnter(Collision collision)
	{
		//health trigger
		if(collision.gameObject.tag == "Player")
		{
			PlayerController.Health -= hitDamage;
			Destroy(gameObject);
			//Debug.Log(PlayerController.Health);
		}
	}
	void MoveMonster()
	{
		target = GameObject.Find("BackgroundCollider");
		if (target != null)
		{
			directionToTarget = (target.transform.position - transform.position).normalized;
			rb.velocity = new Vector3(directionToTarget.x * moveSpeed, 0, directionToTarget.z * moveSpeed);
		}
		else
			rb.velocity = Vector3.zero;
	}
	void PositionalDestroyer()
	{
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		
		if (transform.position.z <= (Player.transform.position.z + 0.5))
		{
			Destroy(gameObject);
		}
	}
	void FollowTarget(int spawner)
	{
		GameObject thisTarget = targetPoints[spawner];
		transform.LookAt(thisTarget.transform);
		if (thisTarget != null)
		{
			transform.position += transform.forward * Time.deltaTime * moveSpeed;
		}
		else
			rb.velocity = Vector3.zero;
		
		//Debug.Log(thisTarget.transform.name + ":" + spawner);
	}
}