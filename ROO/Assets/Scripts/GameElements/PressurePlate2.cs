using System.Collections.Generic;
using UnityEngine;

public class PressurePlate2 : MonoBehaviour
{
    // An array of objects that will be triggered when the weight requirements are met.
    [SerializeField]
    private TriggerAble2[] triggeredObjects;

    // The required weight that needs to be on the pressure plate for it to trigger the objects.
    [SerializeField]
    private int weightRequirment = 1;

    // A list to store all the character scripts that are currently on the pressure plate.
    private List<CharacterScript> characterScripts = new List<CharacterScript>();

    // A list to store the weight of each character that is currently on the pressure plate.
    private List<int> characterOldRelatedWeights = new List<int>();

    // The total weight on the pressure plate.
    public int weightOnMe;

    // A flag to keep track of whether there is a statue on the pressure plate.
    private bool statued;

    // The header for the section related to two pressure plate system.
    [Header("Two Pressure Plate System")]

    // A flag to indicate whether this pressure plate is part of a two pressure plate system.
    [SerializeField]
    private bool twoPressurePlateSystem;

    // A reference to the other pressure plate in the two pressure plate system.
    [SerializeField]
    private PressurePlate2 otherPressurePlate;

    // A flag to keep track of whether the pressure plate has been triggered or not.
    private bool triggered;


    // Called when an object enters the trigger collider attached to this object.
    private void OnTriggerEnter(Collider other)
    {
        // If the object that entered the trigger is either tagged "1" or "2".
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // Get the CharacterScript component attached to the object.
            CharacterScript character = other.GetComponentInParent<CharacterScript>(); ;

            // If the character script is not in the characterScripts list.
            if (!characterScripts.Contains(character))
            {
                // Add the weight of the character to the total weight on the pressure plate.
                weightOnMe += character.weight;

                // Add the weight of the character to the characterOldRelatedWeights list.
                characterOldRelatedWeights.Add(character.weight);

                // Add the character script to the characterScripts list.
                characterScripts.Add(character);
            }

            // For each triggered object, set the characterScriptsOnMe list to the characterScripts list.
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].characterScriptsOnMe = characterScripts;
        }
        // If the object that entered the trigger is tagged "Statue".
        else if (other.CompareTag("Statue"))
        {
            // Set the statued flag to true.
            statued = true;
        }

        // If the total weight on the pressure plate is greater than or equal to the weight requirement, or if there is a statue on the pressure plate.
        if (weightOnMe >= weightRequirment || statued)
        {
            // For each triggered object, call the Toggle
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(true);
            triggered = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Check if the other object colliding with the trigger has tag "1" or "2".
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();

            // Check if the weight of the character has changed.
            for (int i = 0; i < characterScripts.Count; i++)
            {
                if (characterScripts[i].Equals(character) && character.weight != characterOldRelatedWeights[i])
                {
                    // Update the weight of the pressure plate if the weight of the character has changed.
                    weightOnMe -= characterOldRelatedWeights[i];
                    weightOnMe += character.weight;
                    characterOldRelatedWeights[i] = character.weight;
                }
            }
        }
        // Check if the other object colliding with the trigger has tag "Statue".
        else if (other.CompareTag("Statue"))
            statued = true;

        // Check if the total weight on the pressure plate is less than the required weight and there are no statues on the plate.
        if (weightOnMe < weightRequirment && !statued)
        {
            // Check if this pressure plate is part of a two pressure plate system.
            if (twoPressurePlateSystem)
            {
                // Check if the other pressure plate in the system is not triggered.
                if (!otherPressurePlate.triggered)
                    // Toggle the objects triggered by this pressure plate to false.
                    for (int i = 0; i < triggeredObjects.Length; i++)
                        triggeredObjects[i].Toggle(false);
            }
            else
                // Toggle the objects triggered by this pressure plate to false.
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].Toggle(false);
            // Update the triggered flag to false.
            triggered = false;
        }
        // Check if the total weight on the pressure plate is greater than or equal to the required weight.
        else if (weightOnMe >= weightRequirment)
        {
            // Toggle the objects triggered by this pressure plate to true.
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(true);
            // Update the triggered flag to true.
            triggered = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger collider has the tag "1" or "2"
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // Get the `CharacterScript` component attached to the parent of the object exiting the trigger
            CharacterScript character = other.GetComponentInParent<CharacterScript>();

            // Loop through the `characterScripts` list
            for (int i = 0; i < characterScripts.Count; i++)
            {
                // If the current character in the loop is equal to the `character` that just exited the trigger
                if (characterScripts[i] == character)
                {
                    // Remove the character from the `characterScripts` list
                    characterScripts.RemoveAt(i);

                    // Subtract the weight of the character from `weightOnMe`
                    weightOnMe -= character.weight;

                    // Remove the weight of the character from `characterOldRelatedWeights` list
                    characterOldRelatedWeights.RemoveAt(i);
                }
            }
            // Update the `characterScriptsOnMe` property of each `TriggerAble2` object in `triggeredObjects` with the updated `characterScripts` list
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].characterScriptsOnMe = characterScripts;
        }
        // Check if the object exiting the trigger collider has the tag "Statue"
        else if (other.CompareTag("Statue"))
        {
            // Set `statued` to false
            statued = false;
        }

        // Check if `weightOnMe` is less than `weightRequirment` and `statued` is false
        if (weightOnMe < weightRequirment && !statued)
        {
            // Check if `twoPressurePlateSystem` is true
            if (twoPressurePlateSystem)
            {
                // Check if `otherPressurePlate` is not triggered
                if (!otherPressurePlate.triggered)
                {
                    // Loop through `triggeredObjects` and set each `TriggerAble2` object's state to false
                    for (int i = 0; i < triggeredObjects.Length; i++)
                        triggeredObjects[i].Toggle(false);
                }
            }
            else
            {
                // Loop through `triggeredObjects` and set each `TriggerAble2` object's state to false
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].Toggle(false);
            }
            // Set `triggered` to false
            triggered = false;
        }

        // Check if `characterScripts` list is empty
        if (characterScripts.Count == 0)
        {
            // Set `weightOnMe` to 0
            weightOnMe = 0;
        }
    }
}
