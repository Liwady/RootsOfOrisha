using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate2 : MonoBehaviour
{
    [SerializeField]
    private TriggerAble2[] triggeredObjects;
    [SerializeField]
    private int weightRequirment;

    [SerializeField]
    private List<CharacterScript> characterScripts = new List<CharacterScript>();
    [SerializeField]
    private List<int> characterOldRelatedWeights = new List<int>();

    public int weightOnMe;
    private PlayerManager playerManager;


    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            if (!characterScripts.Contains(character))
            {
                weightOnMe += character.weight;
                characterOldRelatedWeights.Add(character.weight);
                characterScripts.Add(character);
            }
        }
        else if (other.CompareTag("Statue"))
        {
            weightOnMe += 10;
        }

        if (weightOnMe >= weightRequirment)
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].Toggle(true);
            }
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
                    Debug.Log(other.tag + " " + character.weight + " ");
                    weightOnMe -= characterOldRelatedWeights[i];
                    weightOnMe += character.weight;
                    characterOldRelatedWeights[i] = character.weight;
                }
            }
        }
        else if (other.CompareTag("Statue"))
        {
            //??
        }

        if (weightOnMe < weightRequirment)
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].Toggle(false);
            }
        }
        else if (weightOnMe >= weightRequirment) 
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].Toggle(true);
            }
        }
         
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("t");
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
        }
        else if (other.CompareTag("Statue"))
        {
            weightOnMe -= 10;
        }


        if (weightOnMe < weightRequirment)
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].Toggle(false);
            }
        }
    }
}
