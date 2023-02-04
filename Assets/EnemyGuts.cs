using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuts : Enemy
{
    [Header("Child Class Attributes")]
    public float AttackTimer;
    public float MinAttackDistanceFromPlayer;
    public float SwordDownWaitDelay;

    private Animator animator;
    private WeaponParent weaponParent;
    private float currentAttackTimer;

    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
        animator = weaponParent.animator;
    }
    private void Start()
    {
        currentAttackTimer = AttackTimer;
    }
    private void Update()
    {
        currentAttackTimer -= Time.deltaTime;
        if (currentAttackTimer < 0)
        {
            StartCoroutine(Attack());

        }
        Move();
    }
    public IEnumerator Attack()
    {
        if (target == null)
            yield break;

        if ((target.transform.position - transform.position).magnitude <= MinAttackDistanceFromPlayer)
        {
            currentAttackTimer = 100;

            weaponParent.Attack();
            yield return new WaitForSeconds(weaponParent.animator.GetCurrentAnimatorClipInfo(0).Length);
            yield return new WaitForSeconds(SwordDownWaitDelay);
            animator.SetTrigger("SwordUp");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

            currentAttackTimer = AttackTimer;
        }

    }
}
