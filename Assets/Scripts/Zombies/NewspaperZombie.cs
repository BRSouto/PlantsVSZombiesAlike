using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperZombie : ZombieMaster
{
    [SerializeField] private GameObject newspaper;
    [SerializeField] private ParticleSystem rage;

    private int once = 0;

    private void Update()
    {
        ZombieState();
        NoNewspaper();
    }

    private void NoNewspaper()
    {
        if (newspaper) return;
        if(newspaper == null && once == 0)
        {
            speed *= 1.5f;
            delayBetweenAttacks *= 0.75f;
            attackDamage *= 1.5f; 
            myAnim.SetFloat("AttackSpeed", 1.25f);
            rage.Play();
            once++;
        }
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
