using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RespawnPoint : MonoBehaviour
{
    private readonly List<Collider> players = new();
    private PlayerManager playerManager;
    private GameObject Lplayer;
    public Transform[] spawnPoints;
    private Animator animator;
    public bool hasFire;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other) //checks if both players reached the respawnpoint
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (Lplayer != other.gameObject && Lplayer != null)
                EnableResapwnPoint();
            else
                Lplayer = other.gameObject;
        }
    }

    private void EnableResapwnPoint() //enables the respawnPoint
    {
        Lplayer = null;
        playerManager.respawnPoint = this;
        if (hasFire)
        {
            animator.SetTrigger("Start");
            GetComponentInChildren<Light>().enabled = true;
        }
    }
}
