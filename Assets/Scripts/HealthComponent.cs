using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 10.0f;
    //[SerializeField]
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void Resurrect()
    {
        currentHealth = maxHealth;
    }
}
