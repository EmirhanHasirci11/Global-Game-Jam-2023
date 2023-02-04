using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaBoss : MonoBehaviour
{
    [Header("General")]
    public float damage;
    public float speed;
    public GameObject ShurikenPrefab;
    public float AttackTimer;

    [Header("Normal Attack")]
    public float TimeBetweenShuriken;

    [Header("4 Shuriken Attack")]
    public Transform[] shurikenSP;

    private Transform target;
    private Rigidbody2D rb;
    private float currentAttackTimer;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentAttackTimer = AttackTimer;
    }

    private void Update()
    {
        currentAttackTimer -= Time.deltaTime;

        if(currentAttackTimer < 0)
        {
            currentAttackTimer = AttackTimer;
            StartCoroutine(NormalAttack());
        }
    }
    public IEnumerator NormalAttack()
    {
        //Add voice
        //Add animation
        Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);
        yield return new WaitForSeconds(TimeBetweenShuriken);
        Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);
    }
}
