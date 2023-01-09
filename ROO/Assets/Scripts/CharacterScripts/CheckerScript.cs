using System.Collections.Generic;
using UnityEngine;
public class CheckerScript : MonoBehaviour
{
    public GameObject parent;
    private CharacterScript myChar;
    private GameManager gameManager;

    [SerializeField]
    private List<Collider> obstacles = new List<Collider>();

    private void Start()
    {
        myChar = parent.GetComponent<CharacterScript>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstacles.Add(other);
            myChar.canResize = false;
        }
        else if (other.CompareTag("1") || other.CompareTag("2") && !other.CompareTag(parent.tag) && myChar.isHoldingCollectible == true)
        {
            if (myChar.isHoldingCollectible == true)
            {
                myChar.isHoldingCollectible = false;
                if (myChar.typeEF == CollectibleScript.FruitEye.fruit)
                {
                    gameManager.amountOfEyes++;
                    gameManager.eyesText.text = gameManager.amountOfFruit.ToString();
                }
                else if (myChar.typeEF == CollectibleScript.FruitEye.eye)
                {
                    gameManager.amountOfFruit++;
                    gameManager.fruitText.text = gameManager.amountOfFruit.ToString();
                }
            }
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


