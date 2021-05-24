using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargantuan : ZombieMaster
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
            myAnim.SetTrigger("isSquashing");
        }
    }


    protected override void DealDamageEvent()
    {
        victim.transform.localScale = new Vector3(1, 0.3f, 1);
        Invoke("DelayDeath", 0.5f);
    }

    private void DelayDeath()
    {
        victim.GetComponent<PlantMaster>().Death();
    }
}
