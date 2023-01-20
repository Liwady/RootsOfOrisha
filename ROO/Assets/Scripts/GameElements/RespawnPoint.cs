using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private List<Collider> players = new List<Collider>();
    private PlayerManager playerManager;
    public Transform[] spawnPoints;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter(Collider other) //checks if both players reached the respawnpoint
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (!players.Contains(other))
            {
                players.Add(other);
                switch (players.Count)
                {
                    case 0:
                        break;
                    case 1:
                        //change sprite
                        break;
                    case 2:
                        EnableResapwnPoint();
                        //change sprite
                        break;
                }
            }
        }
    }

    private void EnableResapwnPoint() //enables the respawnPoint
    {
        playerManager.respawnPoint = this;
    }
}
