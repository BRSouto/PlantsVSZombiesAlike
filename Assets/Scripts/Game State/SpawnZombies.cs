using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    [SerializeField] private GameObject[] zombies;
    [SerializeField] private Transform[] zombieSpawns;
    [SerializeField] private float initialCooldown;
    [SerializeField] private float cooldownBtwZombies;
    private float manageTime;


    private void Start()
    {
        InvokeRepeating("SpawnZombiesOverTime", initialCooldown, cooldownBtwZombies);
        manageTime = Time.time;
    }

    private void Update()
    {
        Debug();
    }

    void SpawnZombiesOverTime()
    {
        if (Time.time < manageTime + 40)
        {
            GenerateZombies(0, 0, 0.7f);
            print("Zombies Incoming");
        }
        else if (Time.time > manageTime + 40 && Time.time < manageTime + 80)
        {
            GenerateZombies(1, 0, 0.5f);
            print("Newspaper Incoming");
        }
        else if (Time.time > manageTime + 80 && Time.time < manageTime + 120)
        {
            GenerateZombies(2, 1, 0.3f);
            GenerateZombies(2, 2, 0.6f);
            print("Miners Incoming");
        }
        else if (Time.time > manageTime + 120 && Time.time < manageTime + 200)
        {
            GenerateZombies(3, 1, 0.1f);
            GenerateZombies(3, 3, 0.5f);
            print("Gargantuans Incoming");
        }
    }

    private void GenerateZombies(int bestPossibleZombie, int worstPossibleZombie, float chanceToSpawnZombie)
    {
        if(Random.Range(0f,1f) >= chanceToSpawnZombie)
        {
            Instantiate(zombies[Random.Range(worstPossibleZombie, bestPossibleZombie)], zombieSpawns[Random.Range(0,zombieSpawns.Length)].position, Quaternion.identity);
            return;
        }
    }

    private void Debug()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(zombies[0], zombieSpawns[Random.Range(0, zombieSpawns.Length)].position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(zombies[1], zombieSpawns[Random.Range(0, zombieSpawns.Length)].position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(zombies[2], zombieSpawns[Random.Range(0, zombieSpawns.Length)].position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(zombies[3], zombieSpawns[Random.Range(0, zombieSpawns.Length)].position, Quaternion.identity);
        }
    }
}
