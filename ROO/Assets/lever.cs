using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    [SerializeField]
    private TriggerAble[] triggeredObjects;

    private bool toggled = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            CharacterScript character = other.GetComponentInParent<CharacterScript>();
            character.inRangeLever = this;
        }
    }

    public void toggleLever()
    {
        if (toggled == true)
        {
            toggled = false;
        }
        else if (toggled == false)
        {
            toggled = true;
        }
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
