using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit, amountOfEyes;
    public TMP_Text fruitText, eyesText, abilityText;
    public Camera mainCamera;
    public PlayerManager playerManager;

    private void Awake()
    {
        amountOfFruit = 0;
        playerManager = FindObjectOfType<PlayerManager>();
        abilityText.text = playerManager.currentAbility.ToString();
    }
    private void Update()
    {
        abilityText.text = playerManager.currentAbility.ToString();
    }

}
