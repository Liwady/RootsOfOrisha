using UnityEngine;

public class CheckerScript : MonoBehaviour
{
    public GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            parent.GetComponent<CharacterScript>().canResize = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
           parent.GetComponent<CharacterScript>().canResize = true;
        }
    }
}


