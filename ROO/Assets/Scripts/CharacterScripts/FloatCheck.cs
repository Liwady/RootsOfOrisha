using UnityEngine;

public class FloatCheck : MonoBehaviour
{
    [SerializeField]
    private CharacterScript myChar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            myChar.hitWhileFloating = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            myChar.hitWhileFloating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            myChar.hitWhileFloating = false;
        }
    }
}
