using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private CharacterScript myChar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            myChar.isGrounded = true;
        }
        else if (other.CompareTag("Water"))
        {
            myChar.isOnWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            myChar.isGrounded = false;
        }
        else if (other.CompareTag("Water"))
        {
            myChar.isOnWater = false;
        }
    }
}
