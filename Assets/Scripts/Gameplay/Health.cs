using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    GameObject bloodPrefab , blood;
    HealthBar healthbar;
    public float currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithRefference, OnDeathWithRefference;
    [SerializeField]
    public bool isDead = false;
    [SerializeField] GameObject gameOverScreen;

    private void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar>();
        bloodPrefab = GameObject.Find("Blood");
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
        blood = Instantiate(bloodPrefab,transform);
        blood.transform.localPosition = new Vector2(0, 0);
        StartCoroutine(BloodDestroy(blood));
        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithRefference?.Invoke(sender);
        }
        else
        {
            OnDeathWithRefference?.Invoke(sender);
            isDead = true;
            if (gameOverScreen!=null)
            {
                gameOverScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator BloodDestroy(GameObject blood)
    {
        yield return new WaitForSeconds(2);
        Destroy(blood);
    }
    
}
