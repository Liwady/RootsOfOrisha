using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit;
    public TMP_Text fruitText;
    public GameObject character1;
    public GameObject character2;
    private int currentChar;
    private void Awake()
    {
        amountOfFruit = 0;
        currentChar = 0;
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
