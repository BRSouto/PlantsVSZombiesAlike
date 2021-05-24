using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] plants;
    [SerializeField] public List<Transform> positions;
    [SerializeField] public List<bool> isOccupied;
    [SerializeField] public Transform initialPlantPosition;
    [SerializeField] private Text text;
    [SerializeField] private AudioSource music;
    private GameObject objt;
    public static int money = 100;
    private bool boughtIt;
    //private int x;

    public static bool isMovingPlant;

    private void Start()
    {
        //x = 0;
        foreach (Transform position in GameObject.Find("Positions").transform)
        {
            if (position.GetComponent<Transform>())
            {
                positions.Add(position);
                //x++;
            }
        }
        for (int i = 0; i < positions.Count; i++)
        {
            isOccupied.Add(false);
        }

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        money = 100;
        music.Play();
    }

    private void Update()
    {
        
    }

    public void BuyPlant(int plant)
    {
        if(boughtIt == true && isMovingPlant == false)
        {
            boughtIt = false;
            isMovingPlant = true;
            objt = Instantiate(plants[plant], initialPlantPosition.position, Quaternion.identity);
        }
        else if(isMovingPlant == true)
        {
            text.color = Color.white;
            text.text = ("Position your plant before buying another one!");
            StartCoroutine(FadeTo(0f, 1.0f));
        }
    }

    public void SubtractMoney(int cost)
    {
        if (money >= cost && isMovingPlant == false)
        {
            money -= cost;
            boughtIt = true;
        }
        else if (money < cost)
        {
            text.color = Color.white;
            text.text = ("Not enough money!");
            //text.color = Color.Lerp(Color.white, new Color(255, 255, 255, 0), lerpTime * Time.deltaTime);
            StartCoroutine(FadeTo(0f, 1.0f));
        }
    }

    public void Shovel()
    {
        isMovingPlant = true;
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = text.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            text.color = newColor;
            yield return null;
        }
    }
}
