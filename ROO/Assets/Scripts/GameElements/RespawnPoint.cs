
using UnityEngine;

// Create a new class called "RespawnPoint" that inherits from MonoBehaviour.
public class RespawnPoint : MonoBehaviour
{
    // Declare private fields "playerManager", "Lplayer", "animator", and "gameManager".
    private PlayerManager playerManager;
    private GameObject Lplayer;
    private Animator animator;
    private GameManager gameManager;

    // Declare a public field "spawnPoints" which is an array of transform points where the players can respawn.
    public Transform[] spawnPoints;

    // Declare a public field "hasFire" which indicates whether the respawn point has fire or not.
    public bool hasFire;

    // This function is called when the script is first loaded.
    private void Start()
    {
        // Find the PlayerManager, GameManager, and Animator objects.
        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
    }

    // This function is called when a collider enters the trigger area of the object.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to player 1 or player 2.
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // Check if both players have reached the respawn point.
            if (Lplayer != other.gameObject && Lplayer != null)
            {
                // Enable the respawn point.
                EnableResapwnPoint();
            }
            else
            {
                // Update the player that has reached the respawn point.
                Lplayer = other.gameObject;
            }
        }
    }

    // This function enables the respawn point.
    private void EnableResapwnPoint()
    {
        // Reset the Lplayer variable.
        Lplayer = null;

        // Set the respawn point of the player manager to this respawn point.
        playerManager.respawnPoint = this;

        // If the respawn point has fire, play the flame animation and sound, and enable the light component.
        if (hasFire)
        {
            animator.SetTrigger("Start");
            gameManager.PlaySound("flame");
            GetComponentInChildren<Light>().enabled = true;
        }
    }
}
