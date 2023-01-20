using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    PlayerManager playerManager;
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
        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter(Collision collision) //checks if a player collided with the collectible, and sets the player to the appropiate state
    {
        if (collision.gameObject.CompareTag("1") || collision.gameObject.CompareTag("2"))
        {
            if (playerManager.currentCharacter.isHoldingCollectible == false)
            {
                if (typeEF == FruitEye.eye && playerManager.currentCharacter.typeEF == FruitEye.eye)
                {
                    gameManager.amountOfEyes++;
                    gameManager.eyesText.text = gameManager.amountOfFruit.ToString();
                }
                else if (typeEF == FruitEye.eye && playerManager.currentCharacter.typeEF == FruitEye.fruit)
                {
                    //change sprite to holding eye
                    Debug.Log("tt");
                    playerManager.currentCharacter.isHoldingCollectible = true;
                }
                else if (typeEF == FruitEye.fruit && playerManager.currentCharacter.typeEF == FruitEye.fruit)
                {
                    gameManager.amountOfFruit++;
                    gameManager.fruitText.text = gameManager.amountOfFruit.ToString();
                }
                else if (typeEF == FruitEye.fruit && playerManager.currentCharacter.typeEF == FruitEye.eye)
                {
                    //change sprite to holding fruit
                    Debug.Log("ff");
                    playerManager.currentCharacter.isHoldingCollectible = true;
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
