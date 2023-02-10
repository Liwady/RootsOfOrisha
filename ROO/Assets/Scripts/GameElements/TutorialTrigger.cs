
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private GameObject Lplayer;  // the last player who collided with the trigger
    private GameManager gameManager;  // reference to the game manager
    [SerializeField]
    private int trigger;  // the trigger value to send to the game manager

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();  // find the game manager in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))  // check if the collided object is a player
        {
            if (Lplayer != other.gameObject && Lplayer != null)  // check if both players have collided with the trigger
            {
                Lplayer = null;  // reset Lplayer
                gameManager.TutorialTriggers(trigger);  // send the trigger value to the game manager
                Destroy(gameObject);  // destroy the trigger object
                Destroy(this);  // destroy this script
            }
            else
            {
                Lplayer = other.gameObject;  // set Lplayer to the collided object
            }
        }
    }
}
