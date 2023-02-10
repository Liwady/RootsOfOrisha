// Import the System.Collections, System.Collections.Generic, and UnityEngine libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a new class called "resetPackages" that inherits from MonoBehaviour.
public class resetPackages : MonoBehaviour
{
    // Declare a public field "packages" which is an array of game objects that need to be reset.
    public GameObject[] packages;

    // Declare a private field "packagesOriginalPos" which is an array of the original positions of the game objects.
    private Vector3[] packagesOriginalPos;

    // This function is called when the script is first loaded.
    private void Awake()
    {
        // Create a new array with the same length as the "packages" array to store the original positions.
        packagesOriginalPos = new Vector3[packages.Length];

        // Loop through each package in the "packages" array.
        for (int i = 0; i < packages.Length; i++)
        {
            // Store the original position of the package.
            packagesOriginalPos[i] = packages[i].transform.position;
        }
    }

    // This function is called when a collider enters the trigger area of the object.
    private void OnTriggerEnter(Collider other)
    {
        // Loop through each package in the "packages" array.
        for (int i = 0; i < packages.Length; i++)
        {
            // Set the position of the package to its original position.
            packages[i].transform.position = packagesOriginalPos[i];
        }
    }
}