using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : PlantMaster
{
    [SerializeField] private float delayBetweenSuns;
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private Transform sunPosition;
    [SerializeField] private GameObject sun;
    [SerializeField] private AudioSource collectSun;

    private void OnEnable()
    {
        CreateSun();    
    }

    private void Update()
    {
        CollectSun();
    }

    private void CreateSun()
    {
            InvokeRepeating("GenerateSun", delayBetweenSuns, delayBetweenSuns);
    }
    
    private void GenerateSun()
    {
        if (sun == null)
        {
            sun = GameObject.Instantiate(sunPrefab, sunPosition.position, new Quaternion(0, 0, 0, 0));
        }
    }

    private void CollectSun()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Sun" && Input.GetMouseButtonDown(0))
            {
                Destroy(hit.collider.gameObject);
                collectSun.Play();
            }
        }
    }

    public override void Death()
    {
        for (int i = 0; i < gameManager.isOccupied.Count; i++)
        {
            if (gameManager.positions[i].position == transform.position)
            {
                gameManager.isOccupied[i] = false;
                print("aaaaa");
            }
        }
        Destroy(gameObject);
        Destroy(sun);
    }
}
