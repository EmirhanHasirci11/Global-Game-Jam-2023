using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySamurai : Enemy
{
    public float AttackTimer;
    private WeaponParent weaponParent;
    private float currentAttackTimer;
    private void Update()
    {
        currentAttackTimer -= Time.deltaTime;
        if(currentAttackTimer < 0)
        {
            Attack();
        }
        Move();
    }
    public void Attack()
    {
        if (target == null)
            return;

        if((target.transform.position - transform.position).magnitude <= 2)
        {
            weaponParent.Attack();
        }
    }
    private void Awake()
    {
       
        weaponParent = GetComponentInChildren<WeaponParent>();
    }
    
   
}
