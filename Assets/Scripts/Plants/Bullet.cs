using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private AudioSource splat;


    private void Start()
    {
        splat = GameObject.Find("GroundGrass").GetComponent<AudioSource>();    
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 3 || collider.gameObject.CompareTag("Wall") || collider.gameObject.GetComponent<Newspaper>())
        {
            splat.Play();
            Invoke("Destroy", 0.1f);
            if (collider.gameObject.GetComponent<ZombieMaster>())
            {
                collider.gameObject.GetComponent<ZombieMaster>().ApplyDamage(damage);
            }
            if (collider.gameObject.GetComponent<Newspaper>())
            {
                collider.gameObject.GetComponent<Newspaper>().health -= damage;
            }
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
