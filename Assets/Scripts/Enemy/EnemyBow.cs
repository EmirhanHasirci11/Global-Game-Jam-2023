using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : Enemy
{
    [Header("Child Class Attributes")]
    public float AttackTimer;
    private Transform playerLocation;
    public float MaxDistanceFromPlayer;
    public GameObject arrow;
    public float arrowSpeed;
    private GameObject MainHero;
    private Health MainHeroHealth;
    private Vector3 defaultLocalScale;
    private float currentAttackTimer;

    private void Start()
    {
        MainHero = GameObject.FindGameObjectWithTag("Player");
        MainHeroHealth = MainHero.GetComponent<Health>();
        defaultLocalScale = transform.localScale;
        currentAttackTimer = AttackTimer;
    }
    private void Update()
    {
        playerLocation = !MainHeroHealth.isDead ? MainHero.transform : null;
        Vector2 direction;
        direction = ((Vector2)playerLocation.position - (Vector2)transform.position).normalized;
        if (direction.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);

        }
        currentAttackTimer -= Time.deltaTime;
        CheckForPlayer();
        if(currentAttackTimer < 0 && target != null)
        {
            currentAttackTimer = AttackTimer;
            Attack();
        }
        if(target != null)
        {
            if ((target.position - transform.position).magnitude > MinDistanceFromPlayer)
            {
                Move();
            }
            else
            {
                MoveBack();
            }
        }
        
        
    }
    public void MoveBack()
    {
        CheckForPlayer();
        if (target != null)
        {
            if ((transform.position - target.position).magnitude < MaxDistanceFromPlayer)
            {
                rb.velocity = -(target.position - transform.position).normalized * speed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            if ((transform.position - startPos).magnitude > 0.1)
            {
                rb.velocity = (startPos - transform.position).normalized * speed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
    private void Attack()
    {
        //Add voice or animations maybe particles idk
        GameObject sp = Instantiate(arrow, transform.position, Quaternion.identity);
        sp.GetComponent<Rigidbody2D>().velocity = (target.position - transform.position).normalized * arrowSpeed;
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        sp.transform.rotation = Quaternion.Slerp(transform.rotation, q, 1000);

    }
}
