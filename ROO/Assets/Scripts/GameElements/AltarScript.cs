using UnityEngine;

public class AltarScript : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private int fruitRequirment, eyeRequirment;
    [SerializeField]
    private TriggerAble gate;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.amountOfFruit >= fruitRequirment && gameManager.amountOfEyes >= eyeRequirment)
            //change sprite
            gate.Toggle(true);
            //change what camera can see
    }
}
