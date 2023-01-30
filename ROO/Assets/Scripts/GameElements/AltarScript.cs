using UnityEngine;

public class AltarScript : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private int fruitRequirment, eyeRequirment;
    [SerializeField]
    private TriggerAble2 gate;
    private Animator animator;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.amountOfFruit >= fruitRequirment && gameManager.eyeColl)
        {
            animator.SetTrigger("Start");
            if (gate != null)
                gate.Toggle(true);
        }

        //change what camera can see
    }
}
