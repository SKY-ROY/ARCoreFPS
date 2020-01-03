using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 20f;

    public void TakeDamage(float DamageAmount)
    {
        health -= DamageAmount;
        if(health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
