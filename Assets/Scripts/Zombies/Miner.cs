using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : ZombieMaster
{
    bool isDigging = false;


    private void Start()
    {
        MoveDown();
    }
    private void Update()
    {
        ZombieState();
        IgnoreCollision();
    }

    protected override void Walk(float speed)
    {
        if (gameObject && victim == null)
        {
            if(!isDigging) MoveDown();
            GetComponent<Rigidbody>().velocity = Vector3.forward * -speed;
            isDigging = true;
        }
    }

    protected override void Attack()
    {
        if (victim != null && canAttack)
        {
            StartCoroutine(CanAttackDelay());
            Stop();
            MoveUp();
            isDigging = false;
            myAnim.SetTrigger("isShoveling");
        }
    }

    private void MoveUp()
    {
        if (isDigging && transform.position.y < 0.8f)
        {
            transform.Translate(0, transform.localScale.y * 2, 0);
        }
    }

    public void MoveDownEvent()
    {
        if (victim.GetComponent<PlantMaster>().currentHealth <= attackDamage)
        {
            Invoke("MoveDown", 0.3f);
        }
    }

    private void MoveDown()
    {
        if (gameObject)
        {
            transform.Translate(0, -transform.localScale.y, 0);
        }
    }

    protected override void DealDamageEvent()
    {
        victim.GetComponent<PlantMaster>().ApplyDamage(attackDamage);
    }
}
