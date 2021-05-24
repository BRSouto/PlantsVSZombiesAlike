using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantMaster : MonoBehaviour
{
    [Header("Parametros basicos do personagem")]
    [Range(0f, 200f)] [SerializeField] protected float startHealth = 100f;

    [Tooltip("Coloque a vida inicial do personagem")] [Range(0f, 200f)] public float currentHealth;

    [SerializeField] protected Animator myAnim;

    protected GameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

    public virtual void TakeDamage(float Damage)
    {
        currentHealth -= Damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void ApplyDamage(float Damage)
    {
        TakeDamage(Damage);
    }

    public virtual void Death()
    {
        for (int i = 0; i < gameManager.isOccupied.Count; i++)
        {
            if (gameManager.positions[i].position == transform.position)
            {
                gameManager.isOccupied[i] = false;
            }
        }
        Destroy(gameObject);
    }
}
