using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombie : ZombieMaster
{

    private void Update()
    {
        ZombieState();
    }

    protected override void Walk(float speed)
    {
        if (gameObject && victim == null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.forward * -speed;

        }
    }

    protected override void Attack()
    {
        if (victim && canAttack)
        {
            StartCoroutine(CanAttackDelay());
            Stop();
            myAnim.SetTrigger("isAttacking");
        }
    }

    protected override void DealDamageEvent()
    {
        victim.GetComponent<PlantMaster>().ApplyDamage(attackDamage);
    }
}