using System.Collections.Generic;
using UnityEngine;

public class PressurePlate2 : MonoBehaviour
{

    [SerializeField]
    private TriggerAble2[] triggeredObjects;
    [SerializeField]
    private int weightRequirment = 1;


    private List<CharacterScript> characterScripts = new List<CharacterScript>();

    private List<int> characterOldRelatedWeights = new List<int>();


    public int weightOnMe;
    private bool statued;
    [Header("Two Pressure Plate System")]
    [SerializeField]
    private bool twoPressurePlateSystem;
    [SerializeField]
    private PressurePlate2 otherPressurePlate;
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>(); ;
            if (!characterScripts.Contains(character))
            {
                weightOnMe += character.weight;
                characterOldRelatedWeights.Add(character.weight);
                characterScripts.Add(character);
            }
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].characterScriptsOnMe = characterScripts;
        }
        else if (other.CompareTag("Statue"))
        {
            statued = true;
        }

        if (weightOnMe >= weightRequirment || statued)
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(true);
            triggered = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            for (int i = 0; i < characterScripts.Count; i++)
            {
                if (characterScripts[i].Equals(character) && character.weight != characterOldRelatedWeights[i])
                {
                    weightOnMe -= characterOldRelatedWeights[i];
                    weightOnMe += character.weight;
                    characterOldRelatedWeights[i] = character.weight;
                }
            }
        }
        else if (other.CompareTag("Statue"))
        {
            statued = true;
        }

        if (weightOnMe < weightRequirment && !!statued)
        {
            if (twoPressurePlateSystem)
            {
                if (!otherPressurePlate.triggered )
                    for (int i = 0; i < triggeredObjects.Length; i++)
                        triggeredObjects[i].Toggle(false);
            }
            else if()
            {
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].Toggle(false);
            }
            triggered = false;
        }
        else if (weightOnMe >= weightRequirment)
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(true);
            triggered = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();

            for (int i = 0; i < characterScripts.Count; i++)
            {
                if (characterScripts[i] == character)
                {
                    characterScripts.RemoveAt(i);
                    weightOnMe -= character.weight;
                    characterOldRelatedWeights.RemoveAt(i);
                }
            }
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].characterScriptsOnMe = characterScripts;
        }
        else if (other.CompareTag("Statue"))
        {
            statued = false;
        }


        if (weightOnMe < weightRequirment && !statued)
        {
            if (twoPressurePlateSystem)
            {
                if (!otherPressurePlate.triggered)
                    for (int i = 0; i < triggeredObjects.Length; i++)
                        triggeredObjects[i].Toggle(false);
            }
            else
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].Toggle(false);
            triggered = false;
        }

        if (characterScripts.Count == 0)
        {
            weightOnMe = 0;
        }
    }
}
