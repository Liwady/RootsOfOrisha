using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private GameManager gameManager;
    private bool update;
    [SerializeField]
    private Collider myWaterBody;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            update = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            update = false;
            gameManager.currentChar.GetComponentInParent<Rigidbody>().drag = 0;
        }
    }

    private void Update()
    {
        if (update)
        {
            if (gameManager.currentChar.canWalkOnWater)
            {
                myWaterBody.isTrigger = false;
            }
            else
            {
                gameManager.currentChar.GetComponentInParent<Rigidbody>().drag = 10;
                myWaterBody.isTrigger = true;
            }
        }
    }
}
