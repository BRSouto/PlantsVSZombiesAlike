using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : PlantMaster
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int force;
    [SerializeField] private int range;
    [SerializeField]private float delayBetweenShots;
    private bool canShoot = true;
    private bool isHittingZombie;
    LayerMask mask;

    private void OnEnable()
    {
        mask = LayerMask.GetMask("Zombie");
    }

    private void Update()
    {
        Shoot();
        Raycast();
        //Debug.DrawRay(muzzle.position, Vector3.forward * range, Color.red);
    }

    private void Raycast()
    {
        Ray ray = new Ray(muzzle.position, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, mask))
        {
            isHittingZombie = true;
        }
        else
            isHittingZombie = false;
    }

    private void Shoot()
    {
        if (canShoot && isHittingZombie && !GetComponent<MovePlant>().isMovingThisPlant)
        {
            StartCoroutine(ShootDelay());
            myAnim.SetTrigger("isShooting");
        }
    }

    public void ShootEvent()
    {
        GameObject blt = Instantiate(bullet, muzzle.position, Quaternion.identity);
        blt.GetComponent<Rigidbody>().AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    private IEnumerator ShootDelay()
    {
        canShoot = false;

        yield return new WaitForSeconds(delayBetweenShots);
        
        canShoot = true;
    }
}
