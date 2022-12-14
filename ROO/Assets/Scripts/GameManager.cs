using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit;
    public TMP_Text fruitText;
    public TMP_Text abilityText;
    public GameObject character1;
    public GameObject character2;
    private int currentChar;
    public int currentAbility;
    public bool abilityActive;
    private void Awake()
    {
        amountOfFruit = 0;
        currentChar = 0;
        abilityActive = false;
        currentAbility = 1;
        abilityText.text = currentAbility.ToString();
    }
    public void SwitchCharacter()
    {
        if (currentChar == 0)
        {
            character1.GetComponent<CharacterScript>().enabled = false;
            character2.GetComponent<CharacterScript>().enabled = true;
            currentChar = 1;
        }
        else
        {
            character2.GetComponent<CharacterScript>().enabled = false;
            character1.GetComponent<CharacterScript>().enabled = true;
            currentChar = 0;
        }
    }
}
