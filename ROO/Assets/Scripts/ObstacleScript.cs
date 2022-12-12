using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private bool isTall;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager= FindObjectOfType<GameManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checker"))
        {
            isTall = other.gameObject.GetComponent<CheckerScript>().isTall;
            if (isTall)
                gameManager.character2.GetComponent<CharacterScript>().canResize = false;
            else
                gameManager.character1.GetComponent<CharacterScript>().canResize = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Checker"))
        {
            isTall = other.gameObject.GetComponent<CheckerScript>().isTall;
            if (isTall)
                gameManager.character2.GetComponent<CharacterScript>().canResize = true;
            else
                gameManager.character1.GetComponent<CharacterScript>().canResize = true;
        }
    }
}
