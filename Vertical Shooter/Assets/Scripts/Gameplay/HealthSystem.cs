using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private string tagName;

    private float currentHealth;

    public bool isEnemy = true;

    public float maxHealth = 10f;

    public GameObject hitEffect, healthBar;

    private DeathSystem deathScript;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (isEnemy == true) 
        {
            tagName = "Bullet";
        }
        else 
        {
            tagName = "EnemyBullet";
        }

        currentHealth = maxHealth;
    }

    private void Start()
    {
        deathScript = GetComponent<DeathSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName)) 
        {
            Vector3 triggerPosition = other.ClosestPointOnBounds(transform.position);

            Vector3 direction = triggerPosition - transform.position;

            GameObject vFX = PoolingManager.instance.UseObject(hitEffect, triggerPosition, Quaternion.LookRotation(direction));

            PoolingManager.instance.ReturnObject(vFX, 1f);

            float damage = float.Parse(other.name);

            TakeDamage(damage);

            PoolingManager.instance.ReturnObject(other.gameObject);
        }
    }

    public void TakeDamage(float damage) 
    {
        currentHealth -= damage;

        CheckHealth();
    }

    void CheckHealth() 
    {
        if (currentHealth <= 0f) 
        {
            if(deathScript != null) 
            {
                deathScript.Death();
            }
        }
    }
}
