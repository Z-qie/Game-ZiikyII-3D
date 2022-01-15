using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{

    public float originalHealth;
    public float currentHealth;
    public UnityAction OnDeath;


    protected virtual void Start()
    {
        currentHealth = 0;
        originalHealth = 100;
    }

    protected virtual void Update()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }


}