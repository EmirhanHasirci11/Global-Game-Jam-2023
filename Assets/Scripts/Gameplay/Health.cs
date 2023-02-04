using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    HealthBar healthbar;
    public float currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithRefference, OnDeathWithRefference;
    [SerializeField]
    public bool isDead = false;

    private void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar>();
    }
    public void InitializeHealth(float healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }
    public void GetHit(float amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        if (healthbar != null)
        {
            healthbar.Damage(currentHealth, amount, maxHealth);
        }
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
