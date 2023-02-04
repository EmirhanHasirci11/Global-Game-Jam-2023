using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Endless : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] float radius;
    [SerializeField] TextMeshProUGUI waitText, waveText;
    bool cooldown;
    int wave;
    int maxenemy;
    void Update()
    {
        if (!AreThereEnemies()&&cooldown == false)
        {
            StartCoroutine(Cooldown());
        }
    }
    private bool AreThereEnemies()
    {
        foreach (Collider2D item in Physics2D.OverlapBoxAll(transform.position, new Vector2(radius, radius), 0))
        {
            if (item.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                return true;
            }
        }

        return false;
    }
    void EnemySpawner()
    {
        GameObject enemy;
        for (int i = 0; i < Random.Range(1, maxenemy+1); i++)
        {
            enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);
            enemy.transform.position = new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
        }
    }
    IEnumerator Cooldown()
    {
        wave++;
        waveText.SetText("Wave " + wave);
        cooldown = true;
        waitText.gameObject.SetActive(true);
        int time = 0;
        waitText.SetText("5");
        while (time<=5)
        {
            yield return new WaitForSeconds(1);
            time++;
            waitText.SetText((5 - time).ToString());
        }
        if (wave == 1 || wave == 3 || wave == 6 || wave == 10 || wave == 15 || wave == 20)
        {
            maxenemy++;
        }
        EnemySpawner();
        cooldown = false;
        waitText.gameObject.SetActive(false);
    }

}
