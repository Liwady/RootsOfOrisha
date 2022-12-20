using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit;
    public TMP_Text fruitText;
    public TMP_Text abilityText;
    public GameObject character1;
    public GameObject character2;
    public GameObject currentChar;
    public int currentAbility;
    public bool abilityActive;
    private void Awake()
    {
        amountOfFruit = 0;
        currentChar = character1;
        abilityActive = false;
        currentAbility = 0;
        abilityText.text = currentAbility.ToString();
    }
    private void Update()
    {
        abilityText.text = currentAbility.ToString();
    }
    public void SwitchCharacter()
    {
        if (currentChar == character1)
        {
            character1.GetComponent<CharacterScript>().enabled = false;
            character2.GetComponent<CharacterScript>().enabled = true;
            currentChar = character2;
        }
        else
        {
            character2.GetComponent<CharacterScript>().enabled = false;
            character1.GetComponent<CharacterScript>().enabled = true;
            currentChar = character1;
        }
    }
    public void SwitchAbility()
    {
        if (currentAbility == 0) //size
            currentAbility = 1;
        else //float
            currentAbility = 0;
    }
}
