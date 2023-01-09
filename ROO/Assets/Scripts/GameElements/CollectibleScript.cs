using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    GameManager gameManager;
    public enum FruitEye
    {
        fruit,
        eye
    }
    [SerializeField]
    private FruitEye typeEF;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("1") || collision.gameObject.CompareTag("2"))
        {
            if (gameManager.currentChar.isHoldingCollectible == false)
            {
                if (typeEF == FruitEye.eye && gameManager.currentChar.typeEF == FruitEye.eye)
                {
                    gameManager.amountOfEyes++;
                    gameManager.eyesText.text = gameManager.amountOfFruit.ToString();
                }
                else if (typeEF == FruitEye.eye && gameManager.currentChar.typeEF == FruitEye.fruit)
                {
                    //change sprite to holding eye
                    Debug.Log("tt");
                    gameManager.currentChar.isHoldingCollectible = true;
                }
                else if (typeEF == FruitEye.fruit && gameManager.currentChar.typeEF == FruitEye.fruit)
                {
                    gameManager.amountOfFruit++;
                    gameManager.fruitText.text = gameManager.amountOfFruit.ToString();
                }
                else if (typeEF == FruitEye.fruit && gameManager.currentChar.typeEF == FruitEye.eye)
                {
                    //change sprite to holding fruit
                    Debug.Log("ff");
                    gameManager.currentChar.isHoldingCollectible = true;
                }
                else
                {
                    Debug.LogError("undefined case in collectible script");
                }
                Destroy(gameObject);
                Destroy(this);
            }
        }
    }
}
