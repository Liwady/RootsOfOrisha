using System.Collections.Generic;
using UnityEngine;



public class CheckerScript : MonoBehaviour
{
    public GameObject parent;

    [SerializeField]
    private List<Collider> obstacles = new List<Collider>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstacles.Add(other);
            parent.GetComponent<CharacterScript>().canResize = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstacles.Remove(other);
            if (obstacles.Count == 0)
            {
                parent.GetComponent<CharacterScript>().canResize = true;
            }
        }
    }

    
}


