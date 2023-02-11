using UnityEngine;

public class FloatCheck : MonoBehaviour
{
    // a reference to the CharacterScript component on the parent GameObject
    [SerializeField]
    private CharacterScript myChar;

    private void OnTriggerEnter(Collider other)
    {
        // if the collider entered is tagged as an "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // set hitWhileFloating of the CharacterScript component to true
            myChar.hitWhileFloating = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // if the collider is still tagged as an "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // set hitWhileFloating of the CharacterScript component to true
            myChar.hitWhileFloating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if the collider exited is tagged as an "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // set hitWhileFloating of the CharacterScript component to false
            myChar.hitWhileFloating = false;
        }
    }
}
