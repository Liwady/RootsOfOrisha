using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private GameManager gameManager;
    private bool updateing = false;
    public GameObject myPlayer;



    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        myPlayer = gameManager.character2;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("2"))
        {
            updateing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("2"))
        {
            updateing = false;
        }
    }

    private void Update()
    {
        if (updateing && myPlayer != null)
        {
            if (gameManager.currentChar.gameObject.CompareTag("1"))
            {
                myPlayer.transform.position = new Vector3(transform.position.x, myPlayer.transform.position.y, myPlayer.transform.position.z);
            }
        }

    }
}
