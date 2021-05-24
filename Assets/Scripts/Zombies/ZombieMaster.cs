using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieMaster : MonoBehaviour
{
    [Header("Parametros basicos do personagem")]
    [Range(0f, 500f)] [SerializeField] protected float startHealth = 100f;

    [Tooltip("Coloque a vida inicial do personagem")] [Range(0f, 500f)] public float currentHealth;

    [SerializeField] protected float speed;

    [SerializeField] protected float attackRange;

    [SerializeField] protected float attackDamage;

    [SerializeField] protected float delayBetweenAttacks;

    [SerializeField] protected Animator myAnim;

    [SerializeField] protected Rigidbody myRb;

    [SerializeField] protected AudioSource death;

    protected GameObject victim;

    protected bool canAttack = true;



    protected abstract void Walk(float speed);


    protected abstract void Attack();


    protected void Stop()
    {
        myRb.velocity = Vector3.zero;
    }


    protected abstract void DealDamageEvent();


    protected void IgnoreCollision()
    {
        Physics.IgnoreLayerCollision(7, 3);
        Physics.IgnoreLayerCollision(6, 3);
    }

    protected void ZombieState()
    {
        if (ShootRay(transform.position, Vector3.forward * attackRange, true, Color.blue).collider)
        {
            if (ShootRay(transform.position, Vector3.forward * attackRange, true, Color.blue).collider.isTrigger == false)
            {
                if (ShootRay(transform.position, Vector3.forward * attackRange, true, Color.blue).collider.CompareTag("Plant"))
                {
                    victim = ShootRay(transform.position, Vector3.forward * attackRange, false, Color.blue).collider.gameObject;
                    if (!victim.GetComponent<MovePlant>().isMovingThisPlant)
                    {
                        Attack();
                    }
                }
                else if (ShootRay(transform.position, Vector3.forward * attackRange, true, Color.blue).collider.CompareTag("Zombie"))
                {
                    Stop();
                }
            }
            else
            {
                victim = null;
                Walk(speed);
            }
        }
        else
        {
            victim = null;
            Walk(speed);
        }
    }


    protected virtual RaycastHit ShootRay(Vector3 origin, Vector3 direction, bool debugRay, Color raycastColor = new Color())
    {
        Ray ray = new Ray(origin, -direction);
        RaycastHit hit;

        if (debugRay)
        {
            Debug.DrawRay(origin, -direction, raycastColor);
        }

        if (Physics.Raycast(ray, out hit, attackRange))
            return hit;

        return new RaycastHit();
    }


    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    public virtual void ApplyDamage(float damage)
    {
        TakeDamage(damage);
    }


    public IEnumerator CanAttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(delayBetweenAttacks);
        canAttack = true;
    }


    public virtual void Death()
    {
        if (!death.isPlaying)
        {
            death.Play();
        }
        Stop();
        Invoke("Die", death.clip.length);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
