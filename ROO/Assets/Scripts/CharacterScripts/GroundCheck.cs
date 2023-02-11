using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private CharacterScript myChar;


    // This function is called whenever a collider enters the trigger area of the collider attached to the same game object as this script component.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger area has the "Obstacle" tag.
        if (other.CompareTag("Obstacle"))
        {
            // Set the "isGrounded" property of the CharacterScript component to true.
            myChar.isGrounded = true;
        }
    }

    // This function is called whenever a collider stays in the trigger area of the collider attached to the same game object as this script component.
    private void OnTriggerStay(Collider other)
    {
        // Check if the collider that is staying in the trigger area has the "Obstacle" tag.
        if (other.CompareTag("Obstacle"))
        {
            // Set the "isGrounded" property of the CharacterScript component to true.
            myChar.isGrounded = true;
        }
    }

    // This function is called whenever a collider exits the trigger area of the collider attached to the same game object as this script component.
    private void OnTriggerExit(Collider other)
    {
        // Check if the collider that exited the trigger area has the "Obstacle" tag.
        if (other.CompareTag("Obstacle"))
        {
            // Set the "isGrounded" property of the CharacterScript component to false.
            myChar.isGrounded = false;
        }
    }
}
