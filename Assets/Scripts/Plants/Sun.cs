using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.money += 50;
    }
}
