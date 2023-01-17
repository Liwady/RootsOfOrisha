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
    private void OnTriggerEnter(Collider other)
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

    private void EnableResapwnPoint()
    {
        playerManager.respawnPoint = this;
    }
}
