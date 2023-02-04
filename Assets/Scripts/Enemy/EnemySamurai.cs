using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySamurai : Enemy
{
    public float AttackTimer;
    public float MaxDistanceFromPlayer;
    private WeaponParent weaponParent;
    private float currentAttackTimer;
    private bool movingBack;

    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
    }
    private void Start()
    {
        currentAttackTimer = AttackTimer;
    }
    private void Update()
    {
        currentAttackTimer -= Time.deltaTime;
        if(currentAttackTimer < 0)
        {
            movingBack = false;
            Attack();
        }
        if (movingBack)
            MoveBack();
        else
            Move();
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

    public void Attack()
    {
        if (target == null)
            return;

        if((target.transform.position - transform.position).magnitude <= 2)
        {
            currentAttackTimer = AttackTimer;
            movingBack = true;
            weaponParent.Attack();
        }
    }
    
    
   
}
