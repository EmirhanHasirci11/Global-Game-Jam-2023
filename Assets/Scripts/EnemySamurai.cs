using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySamurai : Enemy
{
    public float AttackTimer;

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
            
        }
    }
}
