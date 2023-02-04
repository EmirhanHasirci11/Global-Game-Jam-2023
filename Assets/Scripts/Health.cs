using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithRefference, OnDeathWithRefference;
    [SerializeField]
    public bool isDead = false;
    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }
    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithRefference?.Invoke(sender);
        }
        else
        {
            OnDeathWithRefference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }
}
