// Include the Unity engine library.
using UnityEngine;

// Define a new class called "LevelEndScript".
public class LevelEndScript : MonoBehaviour
{
    // Declare a private variable "Lplayer" that will store a reference to the player GameObject.
    private GameObject Lplayer;

    // Declare two serialized fields "gameManager" and "loadingscreen" that can be set in the inspector window of Unity.
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject loadingscreen, overlay;

    // Declare a public boolean variable "eshu".
    public bool eshu;

    // This function is called when a collider enters the trigger zone of the object with this script attached.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has a tag of either "1" or "2".
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // Check if the player reference is not equal to the current collider and is not null.
            if (Lplayer != other.gameObject && Lplayer != null)
            {
                // Set player reference to null and disable the overlay object.
                Lplayer = null;
                overlay.SetActive(false);

                // Check if the loading screen object is not null and enable it.
                if (loadingscreen != null)
                    loadingscreen.SetActive(true);

                // Call a function to set the completed level and switch to a new scene.
                SetCompletedLevel();
                gameManager.GoToEshu();
            }
            // If the player reference is equal to the current collider, set player reference to that collider.
            else
                Lplayer = other.gameObject;
        }
    }

    // This function sets the current level as completed.
    private void SetCompletedLevel()
    {
        // Check if the current level is less than the total completed levels.
        if (LevelTracker.level < LevelTracker.completedLevel.Length)
            LevelTracker.completedLevel[LevelTracker.level] = true;
    }
}

