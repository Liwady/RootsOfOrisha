using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // A reference to the PlayerManager object
    private PlayerManager playerManager;

    // Find and store a reference to the PlayerManager object in the scene when the script is first loaded
    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Triggered when an object enters the trigger area of the DeathZone object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger area is tagged as "1" or "2" (i.e. one of the player characters)
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // Call the RespawnCharacters method in the PlayerManager to respawn the player characters
            playerManager.RespawnCharacters();
        }
    }
}
