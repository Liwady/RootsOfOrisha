using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private TriggerAble[] triggeredObjects;
    [SerializeField]
    private int weightRequirment;
    private bool active, moved;
    private GameObject triggeredChar;


    private void Update()
    {
        for (int i = 0; i < triggeredObjects.Length; i++)
            triggeredChar = triggeredObjects[0].GetComponent<TriggerAble>().triggeredChar;
    }
    private void OnTriggerEnter(Collider other)
    {
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

    private void OnTriggerStay(Collider other)
    {
        CharacterScript character = other.GetComponentInParent<CharacterScript>();

        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (moved)
            {
                if (!active && character.weight >= weightRequirment)
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        moved = true;
        if ((other.CompareTag("1") || other.CompareTag("2")) && triggeredChar == other.gameObject)
        {
            active = false;
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(false);
        }
    }
}
