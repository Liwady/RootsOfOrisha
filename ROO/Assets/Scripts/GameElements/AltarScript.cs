using UnityEngine;

public class AltarScript : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private int fruitRequirment, eyeRequirment;
    [SerializeField]
    private TriggerAble2[] gates;
    private Animator animator;
    private bool toggled;

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
            if (gates != null && toggled == false)
            {
                for (int i = 0; i < gates.Length; i++)
                    gates[i].Toggle(true);
                toggled= true;
                gameManager.PlaySound("flame");
            }
        }

        //change what camera can see
    }
}
