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
	public float maxSpeed = 10f;
	public int hitDamage = 1;
	public int spawnerSignature, targetCount; 
	public float timeSinceLevelStart, moveSpeed, rotFrequency = 50f;
	public char creatureType = 'P';
	public static bool speedShift = true;
	public static float staticChangedSpeed = 1.5f, staticTimePassed, staticMaxSpeed;

	void Start()
	{
		targetCount = GameObject.Find("TargetPoints").transform.childCount;

		FPSScene = GameObject.Find("FPSSceneControl");
		
		targetPoints = new GameObject[targetCount];
		for (int i = 0; i < targetCount; i++)
		{
			targetPoints[i] = GameObject.Find("TargetPoints").transform.GetChild(i).gameObject;
		}

		staticMaxSpeed = maxSpeed;

		rb = GetComponent<Rigidbody>();

		StartCoroutine(SpeedChangeCoroutine());

		moveSpeed = staticChangedSpeed;// Random.Range(1f, 5f);
	}
	void Update()
	{
		FollowTarget(spawnerSignature);
		PositionalDestroyer();
		
		if(FPSScene)
		{
			timeSinceLevelStart = FPSScene.GetComponent<FPSSceneControl>().timeSinceLevelStart;
			staticTimePassed = timeSinceLevelStart;
			//Debug.Log("time since level start: " + timeSinceLevelStart);
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		//health trigger
		if(collision.gameObject.tag == "Player")
		{
			FPSScene.GetComponent<FPSSceneControl>().CamShake();
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
	static public void ResetParameters()
	{
		staticChangedSpeed = 1.5f;
	}
	static IEnumerator SpeedChangeCoroutine()
	{
		Debug.Log("timePassed: " + staticTimePassed);
		if ((int)staticTimePassed % 30 == 0 && staticChangedSpeed < staticMaxSpeed)
		{
			staticChangedSpeed += 0.5f;
			Debug.Log("Speed Increased " + staticChangedSpeed);
		}
		yield return null;//new WaitForSeconds(1f);
	}
}