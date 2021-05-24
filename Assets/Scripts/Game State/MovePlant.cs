using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlant : MonoBehaviour
{
    [SerializeField] private AudioSource lift;
    [SerializeField] private AudioSource placeDown;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 currentPosition;
    private Vector3 lastPosition;
    private Vector3 cursorScreenPoint;
    private Vector3 cursorPosition;
    GameManager gameManager;
    public bool isMovingThisPlant;

    private void Start()
    {
         gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

    void OnMouseDown()
    {
        if (GameManager.isMovingPlant)
        {
            lift.Play();

            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - 
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            isMovingThisPlant = true;

            if (isMovingThisPlant)
            {
                GetComponent<BoxCollider>().isTrigger = true;
            }
            //ChangeOpacity();

            for (int i = 0; i < gameManager.isOccupied.Count; i++)
            {
                if(gameManager.positions[i].position == transform.position)
                    gameManager.isOccupied[i] = false;
            }
        }
    }

    void OnMouseDrag()
    {
        if (GameManager.isMovingPlant)
        {
            GetClosestPosition();
        }
    }

    public void GetClosestPosition()
    {
        cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

        for (int i = 0; i < gameManager.positions.Count; i++)
        {
            if (Vector3.Distance(cursorPosition, gameManager.positions[i].position) < Vector3.Distance(cursorPosition, currentPosition) && gameManager.isOccupied[i] == false)
            {
                lastPosition = currentPosition;
                currentPosition = gameManager.positions[i].position;
                transform.position = currentPosition;
            }
        }
    }

    private void OnMouseUp()
    {
        GameManager.isMovingPlant = false;
        GetComponent<BoxCollider>().isTrigger = false;
        isMovingThisPlant = false;
        placeDown.Play();
        //ChangeOpacity();

        for (int i = 0; i < gameManager.positions.Count; i++)
        {
            if (gameManager.positions[i].position == transform.position)
            {
                gameManager.isOccupied[i] = true;
            }
        }
    }

    //private void ChangeOpacity()
    //{
    //    if (isMovingThisPlant)
    //    {
    //        AlphaValue(0.5f);    
    //    }
    //    if (!isMovingThisPlant)
    //    {
    //        AlphaValue(1f);
    //    }
    //}

    //private void AlphaValue(float finalOpacity)
    //{
    //    float a;
    //    foreach (MeshRenderer renderer in gameObject.transform)
    //    {
    //        if (renderer.GetComponent<MeshRenderer>())
    //        {
    //            a = renderer.material.color.a;
    //            a = finalOpacity;
    //        }
    //    }
    //}
}
