using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{

    public SpriteRenderer weaponRenderer;
    private SpriteRenderer characterRenderer;
    public GameObject playerObject;
    private GameObject MainHero;
    private Vector3 defaultLocalScale;
    private Health MainHeroHealth;
    public Vector2 PointerPosition { get; set; }
    public Animator animator;
    public Transform circleOrigin;
    public Transform swordTip;
    public Transform playerLocation;
    [SerializeField] private float thrustDelay;
    public float radius;
    public float radiusOfTip;
    public float delay = 0.1f;
    public float damage;
    public bool attackBlocked;
    public bool isEnemy;
    private bool thrustBlocked;
    public bool IsAttacking { get; private set; }
    public bool IsSpinn { get; private set; }
    public bool IsThrust { get; private set; }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }
    public void ResetIsSpin()
    {
        IsSpinn = false;
    }
    private void Start()
    {
        characterRenderer = playerObject.GetComponent<SpriteRenderer>();
        defaultLocalScale = playerObject.transform.localScale;
        MainHero = GameObject.FindGameObjectWithTag("Player");
        MainHeroHealth = MainHero.GetComponent<Health>();
        //7 is for Enemy layer in inspector window
        isEnemy = (playerObject.layer == 7) ? true : false;
    }

    private void Update()
    {
        if (IsAttacking || IsSpinn)
            return;

        Vector2 direction;

        playerLocation = !MainHeroHealth.isDead ? MainHero.transform : null;
        if (isEnemy && playerLocation != null)
        {
            direction = ((Vector2)playerLocation.position - (Vector2)transform.position).normalized;

        }
        else
        {
            direction = (PointerPosition - (Vector2)transform.position).normalized;
        }

        transform.right = direction;
        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            playerObject.transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
            if (playerObject.GetComponent<EnemyGuts>() != null)
            {
               scale.y = 1;                
            }
            else
            {
                scale.x = -1;
                scale.y = -1;

            }
        }
        else
        {
            playerObject.transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
            scale.y = 1;
            scale.x = 1;
        }
        transform.localScale = scale;
        //Changes orderInLayer for hiding weapon in some angles
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }
    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }
    public void Thrust()
    {
        if (thrustBlocked)
            return;
        animator.SetTrigger("Thrust");
        IsThrust = true;
        thrustBlocked = true;
        StartCoroutine(DelayThrust(thrustDelay));
    }
    public void Spin()
    {

        animator.SetTrigger("Spin");
        IsSpinn = true;
        StartCoroutine(DelaySpin());

    }

    public void Attack(float delayTime)
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack(delayTime));
    }

    private IEnumerator DelaySpin()
    {
        yield return new WaitForSeconds(2);
        IsSpinn = false;
    }
    private IEnumerator DelayThrust(float thrustDelay)
    {
        yield return new WaitForSeconds(thrustDelay);
        thrustBlocked = false;
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    private IEnumerator DelayAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        attackBlocked = false;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);

        Gizmos.color = Color.yellow;
        Vector3 positionOfSwordTip = swordTip == null ? Vector3.zero : swordTip.position;
        Gizmos.DrawWireSphere(positionOfSwordTip, radiusOfTip);
    }
    public void DetectColliders()
    {
        if (!IsThrust)
        {
            foreach (Collider2D item in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
            {
                Health health;
                if (health = item.GetComponent<Health>())
                {
                    health.GetHit(damage, transform.parent.gameObject);
                }
            }
        }
        else
        {

            foreach (Collider2D item in Physics2D.OverlapCircleAll(swordTip.position, radiusOfTip))
            {
                Health health;
                if (health = item.GetComponent<Health>())
                {
                    health.GetHit(damage, transform.parent.gameObject);
                }
            }
        }
    }
}
