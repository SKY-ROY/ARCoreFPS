using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject target;
    private float moveSpeed;
    public float rotFrequency = 50f;

    Vector3 directionToTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = Random.Range(8f, 10f);
        MoveSpit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //health trigger
        if (collision.gameObject.CompareTag("Player"))//tag == "Player")
        {
            Debug.Log("health--");
            PlayerController.Health -= 5;
            Destroy(gameObject);
        }
    }

    void MoveSpit()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera");
        if (target != null)
        {
            directionToTarget = (target.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(directionToTarget.x * moveSpeed, directionToTarget.y * moveSpeed, directionToTarget.z * moveSpeed);
        }
        else
            rb.velocity = Vector3.zero;
    }
}
