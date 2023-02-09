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
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (typeEF == FruitEye.eye)
                gameManager.UpdateEye(true);
            else if (typeEF == FruitEye.fruit)
            {
                gameManager.amountOfFruit++;
                gameManager.UpdateFruit(gameManager.amountOfFruit);
            }
            gameManager.PlaySound("collect");
            Destroy(gameObject);
            Destroy(this);
        }
    }
}


/* 
 * //&& playerManager.currentCharacter.typeEF == FruitEye.fruit  
 * //&& playerManager.currentCharacter.typeEF == FruitEye.eye  
 * else if (typeEF == FruitEye.eye)//&& playerManager.currentCharacter.typeEF == FruitEye.fruit
                {
                    //change sprite to holding eye
                    Debug.Log("tt");
                    playerManager.currentCharacter.isHoldingCollectible = true;
                }
                else if (typeEF == FruitEye.fruit && playerManager.currentCharacter.typeEF == FruitEye.eye)
                {
                    //change sprite to holding fruit
                    Debug.Log("ff");
                    playerManager.currentCharacter.isHoldingCollectible = true;
                }
 */
