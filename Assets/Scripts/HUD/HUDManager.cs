using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text money;
    [SerializeField] private GameObject victoryScreen;
    private float timeManager;


    private void Start()
    {
        timeManager = Time.time;
    }

    void Update()
    {
        money.text = GameManager.money.ToString();
        if (Time.time >= timeManager + 200) Victory();
    }

    void Victory()
    {
        if(FindObjectOfType<ZombieMaster>() == null)
        {
            print("aaaaa");
            victoryScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        if (victoryScreen.activeSelf)
        {
            victoryScreen.SetActive(false);
        }
        SceneManager.LoadScene(0);
    }
}
