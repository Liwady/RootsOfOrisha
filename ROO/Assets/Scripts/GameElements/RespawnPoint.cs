
using UnityEngine;


public class RespawnPoint : MonoBehaviour
{
    private PlayerManager playerManager;
    private GameObject Lplayer;
    public Transform[] spawnPoints;
    private Animator animator;
    private GameManager gameManager;
    public bool hasFire;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
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
            gameManager.PlaySound("flame");
            GetComponentInChildren<Light>().enabled = true;
        }
    }
}
