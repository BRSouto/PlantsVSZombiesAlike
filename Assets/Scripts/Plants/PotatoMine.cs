using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : PlantMaster
{
    [SerializeField] private bool isActive;
    [SerializeField] private int damage;
    [SerializeField] private float timeToSetUp;
    [SerializeField] private Vector3 distanceToGoUp;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioSource explosionSound;

    private void OnEnable()
    {
        Invoke("Activate", timeToSetUp);
    }

    private void Activate()
    {
        isActive = true;
        transform.position = Vector3.Lerp(transform.position, transform.position + distanceToGoUp, 1f);
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Zombie" && isActive)
        {
            collider.gameObject.GetComponent<ZombieMaster>().ApplyDamage(damage);
            explosion.Play();
            explosionSound.Play();
            Invoke("Death", explosion.main.duration);
        }
    }

    public override void Death()
    {
        for (int i = 0; i < gameManager.isOccupied.Count; i++)
        {
            if (gameManager.positions[i].position + distanceToGoUp == transform.position)
            {
                gameManager.isOccupied[i] = false;
                Destroy(gameObject);
            }
        }
        
    }
}
