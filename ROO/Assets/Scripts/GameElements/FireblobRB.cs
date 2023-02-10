using UnityEngine;

public class FireblobRB : MonoBehaviour
{
    // A reference to the Fireblob's Rigidbody component
    private Rigidbody myRB;

    // A reference to the PlayerManager object
    private PlayerManager playerManager;

    // A reference to the GameManager object
    private GameManager gameManager;

    // The starting y-position of the Fireblob object
    private float startposy;

    // The amount of force to apply to the Fireblob when it falls below its starting y-position
    [SerializeField]
    public int force;

    // Find and store references to the GameManager, Rigidbody, and PlayerManager objects in the scene when the script is first loaded, and store the starting y-position of the Fireblob
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        myRB = this.GetComponent<Rigidbody>();
        playerManager = FindObjectOfType<PlayerManager>();
        startposy = this.transform.position.y;
    }

    // Called once per frame
    void Update()
    {
        // Check if the Fireblob's current y-position is below its starting y-position
        if (this.transform.position.y < startposy)
        {
            // Play a sound effect in the game
            gameManager.PlaySound("lava");

            // Apply a vertical force to the Fireblob to make it rise back up
            myRB.velocity = new Vector3(0, force, 0);
        }
    }

    // Triggered when an object enters the Fireblob's trigger area
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
