using UnityEngine;

public class AltarScript : MonoBehaviour
{
    private GameManager gameManager;        // A reference to the GameManager object
    [SerializeField]
    private int fruitRequirment, eyeRequirment;     // The number of fruit and eyes required to activate the altar
    [SerializeField]
    private TriggerAble2[] gates;         // An array of gate objects that can be toggled
    private Animator animator;            // A reference to the animator component attached to the altar
    private bool toggled;                 // A flag to track whether the gates have already been toggled

    private void Start()
    {
        // Find and store a reference to the GameManager object in the scene
        gameManager = FindObjectOfType<GameManager>();

        // Find and store a reference to the animator component attached to the altar
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the required number of fruit and eyes have been collected
        if (gameManager.amountOfFruit >= fruitRequirment && gameManager.eyeColl)
        {
            // Trigger the altar animation
            animator.SetTrigger("Start");

            // Toggle any gates in the array, if there are any, and if they haven't already been toggled
            if (gates != null && toggled == false)
            {
                for (int i = 0; i < gates.Length; i++)
                    gates[i].Toggle(true);
                toggled = true;

                // Play a sound effect
                gameManager.PlaySound("flame");
            }
        }
    }
}
