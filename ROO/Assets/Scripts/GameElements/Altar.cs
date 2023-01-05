using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private int fruitRequirment;
    [SerializeField]
    private int eyeRequirment;
    [SerializeField]
    private TriggerAble gate;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.amountOfFruit >= fruitRequirment && gameManager.amountOfEyes >= eyeRequirment)
        {
            //change sprite
            gate.Toggle(true);
            //change what camera can see
        }
    }
}
