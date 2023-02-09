using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public enum FruitEye
    {
        fruit,
        eye
    }
    [SerializeField]
    private FruitEye typeEF;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2")) //if triggered by the character
        {
            if (typeEF == FruitEye.eye) // if u are an eye 
                gameManager.UpdateEye(true); //set eye collected
            else if (typeEF == FruitEye.fruit) //if u are a fruit
            {
                gameManager.amountOfFruit++;//fruitcount +1
                gameManager.UpdateFruit(gameManager.amountOfFruit);//give the fruitcount to the game manager 
            }
            gameManager.PlaySound("collect"); //make collect sound 
            Destroy(gameObject);
            Destroy(this);
        }
    }
}

