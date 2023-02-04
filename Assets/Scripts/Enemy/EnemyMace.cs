using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMace : Enemy
{
    [Header("Child Class Attributes")]
    public float AttackTimer;
    public float MinAttackDistanceFromPlayer;
    public int AttackTurnCount;
    private WeaponParent weaponParent;
    private float currentAttackTimer;
    
    private void Start()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
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
            for (int i = 0; i < AttackTurnCount; i++)
            {
                weaponParent.Attack();
                yield return new WaitForSeconds(weaponParent.animator.GetCurrentAnimatorClipInfo(0).Length);
            }
            currentAttackTimer = AttackTimer;
        }
        
    }
}
