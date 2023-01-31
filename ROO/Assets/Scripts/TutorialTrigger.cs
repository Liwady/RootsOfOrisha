using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private GameObject Lplayer;
    private GameManager gameManager;
    [SerializeField]
    private int trigger;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {

            if (Lplayer != other.gameObject && Lplayer != null)
            {
                Lplayer = null;
                gameManager.TutorialTriggers(trigger);
                Destroy(gameObject);
                Destroy(this);
            }
            else
                Lplayer = other.gameObject;
        }
    }
}
