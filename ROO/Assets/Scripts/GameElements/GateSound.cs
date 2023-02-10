using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSound : MonoBehaviour
{
    // Reference to the GameManager component
    private GameManager gameManager;

    // Store the original position of the gate
    private Vector3 originalPos;

    // A flag to keep track of whether the gate has been triggered
    private bool triggered;

    void Awake()
    {
        // Get a reference to the GameManager component
        gameManager = FindObjectOfType<GameManager>();

        // Store the original position of the gate
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If the position of the gate has changed
        if (transform.position != originalPos)
        {
            // Play the gate sound
            gameManager.PlaySound("gate");

            // Update the original position of the gate
            originalPos = transform.position;

            // Set the triggered flag to true
            triggered = true;
        }
        // If the position of the gate has not changed
        else if (transform.position == originalPos)
        {
            // If the gate has been triggered previously
            if (triggered)
            {
                // Stop playing the gate sound
                gameManager.StopSound("gate");

                // Set the triggered flag to false
                triggered = false;
            }
        }
    }
}
