using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private TriggerAble[] triggeredObjects;
    [SerializeField]
    private int weightRequirment;
    [SerializeField]
    private bool canAcceptStatue;

    private bool active, moved;
    public GameObject triggeredChar;
    private List<CharacterScript> characterScripts = new List<CharacterScript>();
    private List<int> characterOldRelatedWeights = new List<int>();
    public int weightOnMe;
    private PlayerManager playerManager;
    private bool statueOn;


    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Update()
    {
        triggeredChar = triggeredObjects[0].GetComponent<TriggerAble>().triggeredChar;
    }
    private void OnTriggerEnter(Collider other) //checks if something steps on the pressure plate
    {

        if (other.CompareTag("Statue") && canAcceptStatue)
        {
            active = true;
            statueOn = true;
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].GetComponent<TriggerAble>().triggeredChar = other.gameObject;
                triggeredObjects[i].Toggle(true);
            }
            moved = false;
        }

        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            if (!characterScripts.Contains(character))
            {
                characterScripts.Add(other.GetComponentInParent<CharacterScript>());
                characterOldRelatedWeights.Add(character.weight);
                weightOnMe += character.weight;
            }
            if (character.weight >= weightRequirment)
            {
                active = true;
                for (int i = 0; i < triggeredObjects.Length; i++)
                {
                    triggeredObjects[i].GetComponent<TriggerAble>().triggeredChar = other.gameObject;
                    triggeredObjects[i].Toggle(true);
                    triggeredObjects[i].weightOnMe = weightOnMe;
                }
            }
            moved = false;
        }

    }
    /*private void OnTriggerStay(Collider other) //checks if something is on the pressure plate
    {
        if (weightRequirment == 8)
        {
            if (other.CompareTag("Statue"))
                return;
        }
        else if (weightOnMe < weightRequirment)
        {
            active = false;
            moved = false;
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(false);
        }
        else
        {
            if (other.CompareTag("1") || other.CompareTag("2") && !statueOn)
            {
                CharacterScript character = other.GetComponentInParent<CharacterScript>();
                if (moved)
                {
                    if (!active && character.weight == weightRequirment)
                    {
                        active = true;
                        moved = false;
                        for (int i = 0; i < triggeredObjects.Length; i++)
                        {
                            triggeredObjects[i].GetComponent<TriggerAble>().triggeredChar = other.gameObject;
                            triggeredObjects[i].Toggle(true);
                            triggeredObjects[i].weightOnMe = weightOnMe;
                        }
                    }
                }
                else if (character.weight < weightRequirment && triggeredChar == other.gameObject)
                {
                    active = false;
                    moved = false;
                    for (int i = 0; i < triggeredObjects.Length; i++)
                        triggeredObjects[i].Toggle(false);
                }
                else if (character.weight == weightRequirment)
                {
                    active = true;
                    moved = false;
                    for (int i = 0; i < triggeredObjects.Length; i++)
                    {
                        triggeredObjects[i].GetComponent<TriggerAble>().triggeredChar = other.gameObject;
                        triggeredObjects[i].Toggle(true);
                        triggeredObjects[i].weightOnMe = weightOnMe;
                    }
                }
            }
        }
    } */

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript chararacter = other.GetComponentInChildren<CharacterScript>();
            for (int i = 0; i < characterScripts.Count; i++)
            {
                if (characterOldRelatedWeights[i] != chararacter.weight && chararacter == characterScripts[i])
                {
                    weightOnMe -= characterOldRelatedWeights[i];
                    weightOnMe += chararacter.weight;
                    characterOldRelatedWeights[i] = chararacter.weight;
                }
            }
            if (weightOnMe < weightRequirment)
            {
                active = false;
                for (int i = 0; i < triggeredObjects.Length; i++)
                {
                    //triggeredObjects[i].GetComponent<TriggerAble>().triggeredChar = other.gameObject;
                    triggeredObjects[i].Toggle(false);
                }
            }
            else
            {
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].weightOnMe = weightOnMe;
            }
        }
        if (other.CompareTag("Statue") && canAcceptStatue)
        {
            active = true;
        }

    }
    private void OnTriggerExit(Collider other) //checks if something leaves the pressure plate
    {
        moved = true;
        if (other.CompareTag("Statue") && statueOn && canAcceptStatue)
        {
            statueOn = false;
            active = false;
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(false);
        }
        else if (statueOn)
            return;

        if ((other.CompareTag("1") || other.CompareTag("2")) /*&& triggeredChar == other.gameObject*/)
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            if (characterScripts.Contains(character))
            {

                if (character.weight == 0)
                {
                    if (playerManager.currentCharacter == playerManager.character1script)
                        weightOnMe -= 4;
                    else if (playerManager.currentCharacter == playerManager.character2script)
                        weightOnMe -= 2;
                }
                else
                    weightOnMe -= character.weight;
            }
            characterScripts.Remove(character);
            characterOldRelatedWeights.Remove(character.weight);
            if (characterScripts.Count == 0)
            {
                active = false;
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].Toggle(false);
            }
            else
            {
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].weightOnMe = weightOnMe;
            }
        }

    }
}
