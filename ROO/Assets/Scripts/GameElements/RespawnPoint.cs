using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private List<Collider> players = new List<Collider>();
    private GameManager gameManager;
    public Transform[] spawnPoints;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        gameManager.respawnPoint = this;
    }
}
