using UnityEngine;

public class LeverScript : MonoBehaviour
{
    [SerializeField]
    private TriggerAble[] triggeredObjects;
    private CharacterScript character;
    private bool toggled = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2") && this != null)
        {
            character = other.GetComponentInParent<CharacterScript>();
            character.inRangeLever = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (character.gameObject.CompareTag(other.tag) && character.inRangeLever != null)
            character.inRangeLever = null;
    }

    public void ToggleLever()
    {
        if (toggled == true)
            toggled = false;
        else
            toggled = true;
    }

    private void Update()
    {
        if (toggled)
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].Toggle(true);
            }
        }
        else
        {
            for (int i = 0; i < triggeredObjects.Length; i++)
            {
                triggeredObjects[i].Toggle(false);
            }
        }
    }
}
