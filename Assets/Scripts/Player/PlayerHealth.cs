using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,Damageable,Healthable
{
    public static System.Action PlayerDeath;
    public static System.Action<int,bool> HealthAmount;

    [SerializeField] private int maxHealth = 100;
    [Header("Death Effect")]
    [SerializeField] private GameObject deathEffect;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void Damage(int damage)
    {
        if(!PlayerController.Instance.IsDead)
        {
            PlayerCameraShake.Instance.StartShake(0.3f, 0.2f);
            currentHealth -= damage;
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            if (currentHealth <= damage)
            {
                currentHealth = 0;
                PlayerDeath?.Invoke();
            }
            HealthAmount?.Invoke(damage,false);
            Debug.Log("Current Health :" + currentHealth);
        }
    }
    public void TakeHealth(int health)
    {
        currentHealth += health;

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        HealthAmount?.Invoke(health,true);
        Debug.Log("Current Health :" + currentHealth);
    }
}
