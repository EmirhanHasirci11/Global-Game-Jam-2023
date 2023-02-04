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
    public float teleportTimer;

    [Header("Normal Attack")]
    public float TimeBetweenShuriken;

    [Header("Quick Attack")]
    public float TimeBetweenQuickAttack;
    public int QuickAttackCount;

    [Header("Random Quick Attack")]
    public float TimeBetweenRandomQuickAttack;
    public int RandomQuickAttackCount;

    [Header("4 Shuriken Attack")]
    public Transform[] shurikenSP;




    private Transform target;
    private Rigidbody2D rb;
    private Health health;
    private float currentAttackTimer;
    private float currentTeleportTimer;
    private Vector3 moveDir;
    private bool canMove;
    private bool isAttacking;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = gameObject.GetComponent<Health>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentAttackTimer = AttackTimer;
        currentTeleportTimer = teleportTimer;
        canMove = true;
    }

    private void Update()
    {
        currentAttackTimer -= Time.deltaTime;
        currentTeleportTimer -= Time.deltaTime;

        if(currentAttackTimer < 0)
        {
            currentAttackTimer = 100;
            if(health.currentHealth > health.maxHealth * 70/100)
            {
                StartCoroutine(NormalAttack());
            }
            else if(health.currentHealth <= health.maxHealth * 70 / 100 && health.currentHealth >= health.maxHealth * 40 / 100)
            {
                AttackTimer = AttackTimer * 3 / 4;
                StartCoroutine(QuickAttack());
            }
            else
            {
                AttackTimer = AttackTimer * 3 / 4;
                StartCoroutine(RandomQuickAttack());
            }
        }
        else
        {
            if(currentTeleportTimer < 0 && !isAttacking)
            {
                currentTeleportTimer = 100;
                canMove = false;
                StartCoroutine(Teleport());
            }
            if(canMove)
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
            moveDir = chooseDir();
        }
    }

    public IEnumerator Teleport()
    {
        rb.velocity = Vector3.zero;
        Vector3 pos = chooseDir();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        //Add efects
        yield return new WaitForSeconds(1);
        transform.position = pos;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;

        currentTeleportTimer = teleportTimer;
        moveDir = chooseDir();
        canMove = true;

    }
    private Vector3 chooseDir()
    {
        float randomX = Random.Range(MapTopLeft.position.x, MapBotRight.position.x);
        float randomY = Random.Range(MapTopLeft.position.y, MapBotRight.position.y);
        return new Vector3(randomX, randomY, 0);
    }
    public IEnumerator NormalAttack()
    {
        //Add voice
        //Add animation
        isAttacking = true;
        Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);
        yield return new WaitForSeconds(TimeBetweenShuriken);
        Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);

        currentAttackTimer = AttackTimer;
        isAttacking = false;
    }

    public IEnumerator QuickAttack()
    {
        for (int i = 0; i < QuickAttackCount; i++)
        {
            //Add voice
            //Add animation
            isAttacking = true;
            Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);
            yield return new WaitForSeconds(TimeBetweenQuickAttack);
            
        }
        isAttacking = false;
        currentAttackTimer = AttackTimer;
    }

    public IEnumerator RandomQuickAttack()
    {
        for (int i = 0; i < RandomQuickAttackCount; i++)
        {
            //Add voice
            //Add animation
            isAttacking = true;
            Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(chooseDir() - transform.position);
            yield return new WaitForSeconds(TimeBetweenRandomQuickAttack);

        }

        isAttacking = false;
        currentAttackTimer = AttackTimer;
    }

}
