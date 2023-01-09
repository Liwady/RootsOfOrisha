using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private TriggerAble[] triggeredObjects;
    [SerializeField]
    private int weightRequirment;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            if (character.weight >= weightRequirment)
            {
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].Toggle(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            if (character.weight < weightRequirment)
            {
                for (int i = 0; i < triggeredObjects.Length; i++)
                    triggeredObjects[i].Toggle(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
                triggeredObjects[i].Toggle(false);
        }
    }
}
