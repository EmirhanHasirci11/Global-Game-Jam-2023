using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image greenhealth;
    [SerializeField] Image redhealth;
    [SerializeField] Animator healthanimator;

    private void Start()
    {
        if (greenhealth==null)
        {
            healthanimator = gameObject.transform.Find("Canvas").transform.Find("PlayerHealthBackground").GetComponent<Animator>();
            greenhealth = healthanimator.gameObject.transform.Find("Green").GetComponent<Image>();
            redhealth = healthanimator.gameObject.transform.Find("RedHealth").GetComponent<Image>();
        }
    }
    public void Damage(float currenthealth, float damage, float maxhealth)
    {
        greenhealth.fillAmount = (currenthealth - damage) / maxhealth;
        StartCoroutine(RedHealth(1, currenthealth, damage,maxhealth));
        healthanimator.Play("PHealthShake");
    }
    IEnumerator RedHealth(float time,float starthealth,float damage,float maxhealth)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < time)
        {
            yield return new YieldInstruction();
            elapsedTime += Time.unscaledDeltaTime;
            redhealth.fillAmount = (starthealth-damage*Mathf.Clamp01(elapsedTime / time))/maxhealth;
        }
    }
}
