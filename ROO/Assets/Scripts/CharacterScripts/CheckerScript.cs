using System.Collections.Generic;
using UnityEngine;

public class CheckerScript : MonoBehaviour
{
    // Reference to the parent game object
    public GameObject parent;
    // Reference to the CharacterScript component on the parent object
    private CharacterScript myChar;

    // A list of obstacle colliders that the checker has entered
    [SerializeField]
    private List<Collider> obstacles = new List<Collider>();

    private void Start()
    {
        // Get a reference to the CharacterScript component on the parent object
        myChar = parent.GetComponent<CharacterScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collider is tagged as an "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // Add it to the list of obstacles
            obstacles.Add(other);
            // Set canResize to false on the CharacterScript component
            myChar.canResize = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the collider is tagged as an "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // Remove it from the list of obstacles
            obstacles.Remove(other);
            // If there are no more obstacles in the list
            if (obstacles.Count == 0)
                // Set canResize to true on the CharacterScript component
                myChar.canResize = true;
        }
    }
}


