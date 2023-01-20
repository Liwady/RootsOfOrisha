using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private PlayerManager playerManager;
    private bool updating = false;
    public GameObject myPlayer;



    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        myPlayer = playerManager.character2;
    }
    private void OnTriggerEnter(Collider other) //checks if player 2 entered the boat
    {
        if (other.CompareTag("2"))
        {
            updating = true;
        }
    }

    private void OnTriggerExit(Collider other) //checks if player 2 exited the boat
    {
        if (other.CompareTag("2"))
        {
            updating = false;
        }
    }

    private void Update()
    {
        if (updating && myPlayer != null)
        {
            if (playerManager.currentCharacter.gameObject.CompareTag("1"))
            {
                myPlayer.transform.position = new Vector3(transform.position.x, myPlayer.transform.position.y, myPlayer.transform.position.z);
            }
        }

    }
}
