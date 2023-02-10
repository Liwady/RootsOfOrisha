using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    // Enum to represent whether the collectible is a fruit or an eye
    public enum FruitEye
    {
        fruit,
        eye
    }

    // The type of the collectible (fruit or eye), set in the Unity editor
    [SerializeField]
    private FruitEye typeEF;

    // A reference to the GameManager object
    private GameManager gameManager;

    // Find and store a reference to the GameManager object in the scene when the script is first loaded
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Triggered when the player character enters the trigger area around the collectible
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that triggered the collectible is tagged as "1" or "2" (i.e. the player character)
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // Check whether the collectible is an eye or a fruit
            if (typeEF == FruitEye.eye)
            {
                // Set the eye collected flag to true in the game manager
                gameManager.UpdateEye(true);
            }
            else if (typeEF == FruitEye.fruit)
            {
                // Increment the fruit count in the game manager by 1 and update the UI
                gameManager.amountOfFruit++;
                gameManager.UpdateFruit(gameManager.amountOfFruit);
            }

            // Play a sound effect
            gameManager.PlaySound("collect");

            // Destroy the collectible object and this script component attached to it
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
