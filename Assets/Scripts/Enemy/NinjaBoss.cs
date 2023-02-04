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
    public Transform MapTopLeft;
    public Transform MapBotRight;

    [Header("Normal Attack")]
    public float TimeBetweenShuriken;

    [Header("4 Shuriken Attack")]
    public Transform[] shurikenSP;



    private Transform target;
    private Rigidbody2D rb;
    private float currentAttackTimer;
    private Vector3 moveDir;

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
        else
        {
            Move();
        }
    }

    public void Move()
    {
        if((transform.position - moveDir).magnitude > 0.1)
        {
            rb.velocity = (moveDir - transform.position).normalized * speed;
        }
        else
        {
            chooseDir();
        }
    }

    private void chooseDir()
    {
        float randomX = Random.Range(MapTopLeft.position.x, MapBotRight.position.x);
        float randomY = Random.Range(MapTopLeft.position.y, MapBotRight.position.y);
        moveDir = new Vector3(randomX, randomY, 0);
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
