using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour
{
    public int health;

    private void Update()
    {
        Destroy();
    }

    void Destroy()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
