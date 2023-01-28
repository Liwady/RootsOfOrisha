using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private TriggerAble[] triggeredObjects;
    [SerializeField]
    private int weightRequirment;
    private bool active, moved;
    public GameObject triggeredChar;


    private void Update()
    {
        for (int i = 0; i < triggeredObjects.Length; i++)
            triggeredChar = triggeredObjects[0].GetComponent<TriggerAble>().triggeredChar;
    }
    private void OnTriggerEnter(Collider other) //checks if something steps on the pressure plate
    {
        if (other.CompareTag("Statue"))
        {
            active = true;
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].GetComponent<TriggerAble>().triggeredChar = other.gameObject;
                triggeredObjects[i].Toggle(true);
            }
        }
        moved = false;
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            if (character.weight >= weightRequirment)
            {
                active = true;
                for (int i = 0; i < triggeredObjects.Length; i++)
                {
                    triggeredObjects[i].GetComponent<TriggerAble>().triggeredChar = other.gameObject;
                    triggeredObjects[i].Toggle(true);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other) //checks if something is on the pressure plate
    {
        if (other.CompareTag("Statue"))
        {
            return;
        }
        if (other.CompareTag("1") || other.CompareTag("2"))
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
                }
            }
        }
    }
    private void OnTriggerExit(Collider other) //checks if something leaves the pressure plate
    {
        moved = true;
        if ((other.CompareTag("1") || other.CompareTag("Statue") || other.CompareTag("2")) && triggeredChar == other.gameObject)
        {
            active = false;
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(false);
        }
    }
}
