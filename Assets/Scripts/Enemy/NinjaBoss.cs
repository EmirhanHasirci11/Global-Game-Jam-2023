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
            currentAttackTimer = 100;
            StartCoroutine(RandomQuickAttack());
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
            moveDir = chooseDir();
        }
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
        Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);
        yield return new WaitForSeconds(TimeBetweenShuriken);
        Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);

        currentAttackTimer = AttackTimer;
    }

    public IEnumerator QuickAttack()
    {
        for (int i = 0; i < QuickAttackCount; i++)
        {
            //Add voice
            //Add animation
            Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(target.position - transform.position);
            yield return new WaitForSeconds(TimeBetweenQuickAttack);
        }

        currentAttackTimer = AttackTimer;
    }

    public IEnumerator RandomQuickAttack()
    {
        for (int i = 0; i < RandomQuickAttackCount; i++)
        {
            //Add voice
            //Add animation
            Instantiate(ShurikenPrefab, transform.position, Quaternion.identity).GetComponent<Shuriken>().GiveSpeed(chooseDir() - transform.position);
            yield return new WaitForSeconds(TimeBetweenRandomQuickAttack);

        }

        currentAttackTimer = AttackTimer;
    }

}
