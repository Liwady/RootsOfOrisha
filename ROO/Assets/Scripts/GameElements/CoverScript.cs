using UnityEngine;

public class CoverScript : MonoBehaviour
{
    // A variable to store the duration of the color change effect
    [SerializeField]
    private float duration;

    // A variable to keep track of the current time for the color change effect
    private float time;

    // A reference to the sprite renderer component
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // A flag to indicate whether the color change effect is currently in progress
    private bool change;

    // A flag to indicate whether the object should be made transparent (true) or opaque (false)
    private bool vanishing;

    // A flag to indicate the initial state of the object (transparent or opaque)
    public bool statue;

    // A variable to keep track of the number of players currently touching the object
    private int numPlayers;

    // The Awake method is called when the script instance is being loaded
    private void Awake()
    {
        // If the statue flag is set to true, the object should be set to opaque
        if (statue)
            vanishing = false;
    }

    // The Update method is called once per frame
    private void Update()
    {
        // If the change flag is set to true, change the color of the object
        if (change)
            ChangeColor(vanishing);
    }

    // A method to change the color of the object
    private void ChangeColor(bool st)
    {
        // If the "st" flag is set to true, make the object transparent
        if (st)
            spriteRenderer.color = Color.Lerp(Color.white, Color.clear, time);
        // If the "st" flag is set to false, make the object opaque
        else
            spriteRenderer.color = Color.Lerp(Color.clear, Color.white, time);

        // If the time variable is less than 1, increase it by the time delta divided by the duration
        if (time < 1)
            time += Time.deltaTime / duration;
        // If the time variable is equal to or greater than 1, stop the color change effect
        else
        {
            change = false;
            time = 0;
        }
    }

    // The OnTriggerEnter method is called when a collider enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        // If the collider has a tag of "1" or "2"
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // If the number of players is currently 0
            if (numPlayers == 0)
            {
                // Start the color change effect
                change = true;

                // If the statue flag is set to false, make the object transparent
                if (!statue)
                    vanishing = true;
                // If the statue flag is set to true, make the object opaque
                else
                    vanishing = false;
            }

            // Increase the number of players
            numPlayers++;
        }
    }

    // The OnTriggerExit method is called when a collider exits the trigger area
    private void OnTriggerExit(Collider other)
    {
        // If the collider has a tag of "1" or "2
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            // Decrease the number of players
            numPlayers--;
            // If the number of players is now 0
            if (numPlayers == 0)
            {
                // Start the color change effect
                change = true;

                // If the statue flag is set to false, make the object opaque
                if (!statue)
                    vanishing = false;
                // If the statue flag is set to true, make the object transparent
                else
                    vanishing = true;
            }
        }
    }
}
