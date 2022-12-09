using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameManager.amountOfFruit++;
        gameManager.fruitText.text = gameManager.amountOfFruit.ToString();
        Destroy(gameObject);
        Destroy(this);
    }
}
