using System.Collections.Generic;
using UnityEngine;
public class CheckerScript : MonoBehaviour
{
    public GameObject parent;
    private CharacterScript myChar;

    [SerializeField]
    private List<Collider> obstacles = new();
    private void Start()
    {
        myChar = parent.GetComponent<CharacterScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstacles.Add(other);
            myChar.canResize = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstacles.Remove(other);
            if (obstacles.Count == 0)
                myChar.canResize = true;
        }
    }


}


