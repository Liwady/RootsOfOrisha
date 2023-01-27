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
    private PlayerManager playerManager;
    private GameManager gameManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("1") || other.gameObject.CompareTag("2"))
        {
            if (playerManager.currentCharacter.isHoldingCollectible == false)
            {
                if (typeEF == FruitEye.eye)
                    gameManager.UpdateEye(true);
                else if (typeEF == FruitEye.fruit)
                {
                    gameManager.amountOfFruit++;
                    gameManager.UpdateFruit(gameManager.amountOfFruit);
                }
                    

                Destroy(gameObject);
                Destroy(this);
            }
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
