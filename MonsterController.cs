using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
	private Rigidbody rb;
	private GameObject target, FPSScene;
	private Vector3 directionToTarget;

	public GameObject[] targetPoints;

	[Header("Control Parameters")]
	public int hitDamage = 1;
	public int spawnerSignature, targetCount; 
	public float timeSinceLevelStart, moveSpeed, rotFrequency = 50f;
	public char creatureType = 'P';
	public static bool speedShift = true;
	public static float changedSpeed = 2f, timePassed;

	void Start()
	{
		targetCount = GameObject.Find("TargetPoints").transform.childCount;

		FPSScene = GameObject.Find("FPSSceneControl");
		
		targetPoints = new GameObject[targetCount];
		for (int i = 0; i < targetCount; i++)
		{
			targetPoints[i] = GameObject.Find("TargetPoints").transform.GetChild(i).gameObject;
		}

		rb = GetComponent<Rigidbody>();

		StartCoroutine(SpeedChangeCoroutine());

		moveSpeed = changedSpeed;// Random.Range(1f, 5f);
	}
	void Update()
	{
		FollowTarget(spawnerSignature);
		PositionalDestroyer();
		
		if(FPSScene)
		{
			timeSinceLevelStart = FPSScene.GetComponent<FPSSceneControl>().timeSinceLevelStart;
			timePassed = timeSinceLevelStart;
			//Debug.Log("time since level start: " + timeSinceLevelStart);
		}

		//Debug.Log((int)timeSinceLevelStart + " " + timeSinceLevelStart);
		//Debug.Log(speedShift);
		//ChangeSpeed();
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
	IEnumerator SpeedChangeCoroutine()
	{
		yield return new WaitForSeconds(5f);
		//speedShift = false;
		
		changedSpeed += 1;
		Debug.Log("changed speed " + changedSpeed);
		Debug.Log((int)timePassed + " " + timePassed);
		
		//Debug.Log(speedShift);
		//speedShift = true;
	}
	public void ChangeSpeed()//function to be called in update()
	{
		if ((int)timeSinceLevelStart % 5 == 0 && speedShift)
		{
			speedShift = false;
			Debug.Log(speedShift);
			//StartCoroutine(SpeedChangeCoroutine());

			//changedSpeed += 0.1f;

			//Debug.Log((int)timeSinceLevelStart + " " + timeSinceLevelStart);
		}
	}
}