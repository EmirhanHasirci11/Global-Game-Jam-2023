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
    public float MinDistancePhase1;

    [Header("Normal Attack")]
    public float TimeBetweenShuriken;

    [Header("Quick Attack")]
    public float quickAttackTimer;
    public float TimeBetweenQuickAttack;
    public int QuickAttackCount;

    [Header("Random Quick Attack")]
    public float randomQuickAttackTimer;
    public float TimeBetweenRandomQuickAttack;
    public int RandomQuickAttackCount;

    [Header("4 Shuriken Attack")]
    public Transform[] shurikenSP;




    private Transform target;
    private Rigidbody2D rb;
    private Health health;
    private GameObject MainHero;
    private Transform playerLocation;
    private float currentAttackTimer;
    private float currentTeleportTimer;
    private Vector3 moveDir;
    private Health MainHeroHealth;
    private bool canMove;
    private bool isAttacking;
    private int phase = 0;
    private Vector3 defaultLocalScale;
    private void Awake()
    {
        MainHero = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = gameObject.GetComponent<Health>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentAttackTimer = AttackTimer;
        MainHeroHealth = MainHero.GetComponent<Health>();
        defaultLocalScale = transform.localScale;
        currentTeleportTimer = teleportTimer;
        canMove = true;
    }

    private void Update()
    {
        playerLocation = !MainHeroHealth.isDead ? MainHero.transform : null;
        Vector2 direction;
        direction = ((Vector2)playerLocation.position - (Vector2)transform.position).normalized;
        if (direction.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);

        }
        currentAttackTimer -= Time.deltaTime;
        currentTeleportTimer -= Time.deltaTime;

        if (health.currentHealth > health.maxHealth * 70 / 100)
            phase = 0;
        else if (health.currentHealth <= health.maxHealth * 70 / 100 && health.currentHealth >= health.maxHealth * 40 / 100)
            phase = 1;
        else
            phase = 2;

        if (currentAttackTimer < 0)
        {
            currentAttackTimer = 100;
            if (phase == 0)
            {
                StartCoroutine(NormalAttack());
            }
            else if (phase == 1)
            {
                AttackTimer = quickAttackTimer;
                StartCoroutine(QuickAttack());
            }
            else if (phase == 2)
            {
                AttackTimer = randomQuickAttackTimer;
                StartCoroutine(RandomQuickAttack());
            }
        }
        else
        {
            if (currentTeleportTimer < 0 && !isAttacking)
            {
                currentTeleportTimer = 100;
                canMove = false;
                StartCoroutine(Teleport());
            }
            if (canMove)
            {
                if (phase == 0)
                    Move();
                else if (phase == 1)
                {
                    if ((transform.position - target.position).magnitude > MinDistancePhase1)
                    {
                        moveDir = target.position;
                        Move();
                    }
                    else
                    {
                        moveDir = chooseDir();
                    }
                }
            }
        }

    }

    public void Move()
    {
        if ((transform.position - moveDir).magnitude > 0.1)
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
