using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit;
    public TMP_Text fruitText;
    public TMP_Text abilityText;
    public GameObject character1;
    public GameObject character2;
    public CharacterScript currentChar;
    public int currentAbility;
    public bool abilityActive;
    private void Awake()
    {
        amountOfFruit = 0;
        currentChar = character1.GetComponent<CharacterScript>();
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
        if (currentChar.gameObject == character1)
        {
            character1.GetComponent<CharacterScript>().enabled = false;
            character2.GetComponent<CharacterScript>().enabled = true;
            currentChar = character2.GetComponent<CharacterScript>();
        }
        else
        {
            character2.GetComponent<CharacterScript>().enabled = false;
            character1.GetComponent<CharacterScript>().enabled = true;
            currentChar = character1.GetComponent<CharacterScript>();
        }
    }
    public void TriggerAbility()
    {
        currentChar.canMove = true;
        switch (currentAbility)
        {
            // size
            case 0:
                currentChar.Sizing();
                break;
            // float
            case 1:
                currentChar.Floating();
                SetGravity();
                break;
        }
        SetWeight();
        SetMovementSpeed();
    }
    public void SwitchAbility()
    {

        if (currentAbility == 0)
        { //size
            if (abilityActive)
            {
                currentChar.Sizing();
                SetGravity();
                abilityActive = false;
            }
            currentAbility = 1;
        }
        else
        {  //float
            if (abilityActive)
            {
                currentChar.Floating();
                abilityActive = false;
            }
            currentAbility = 0;
        }

    }
    public void SetSize()
    {
        if (currentChar.gameObject == character1)
        {
            character1.transform.localScale = character1.transform.localScale * 1.8f;
            character2.transform.localScale = character2.transform.localScale / 1.8f;
        }
        else
        {
            character2.transform.localScale = character2.transform.localScale * 1.2f;
            character1.transform.localScale = character1.transform.localScale / 1.2f;
        }
    }
    public void SetWeight()
    {
        if (abilityActive)
        {
            if (currentChar.gameObject == character1) //char1
            {
                if (currentAbility == 1)//size change
                {
                    character1.GetComponent<CharacterScript>().weight = 5;
                    character2.GetComponent<CharacterScript>().weight = 1;
                }
                else
                {
                    character1.GetComponent<CharacterScript>().weight = 6;
                    character2.GetComponent<CharacterScript>().weight = 0;
                }
            }
            else //char2
            {
                if (currentAbility == 1)//size change
                {
                    character2.GetComponent<CharacterScript>().weight = 4;
                    character1.GetComponent<CharacterScript>().weight = 2;
                }
                else
                {
                    character2.GetComponent<CharacterScript>().weight = 6;
                    character1.GetComponent<CharacterScript>().weight = 0;
                }
            }
        }
        else
        {
            character1.GetComponent<CharacterScript>().weight = 4;
            character2.GetComponent<CharacterScript>().weight = 2;
        }
    }
    public void SetGravity()
    {
        if (currentAbility == 1 && abilityActive)
        {
            if (currentChar.gameObject == character1)
            {
                character1.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
                character2.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                character1.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = false;
                character2.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
            character1.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
            character2.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
        }

    }
    public void SetMovementSpeed()
    {
        if (abilityActive)
        {
            if (currentChar.gameObject == character1) //char1
            {
                if (currentAbility == 0)//size change
                {
                    character1.GetComponent<CharacterScript>().movementSpeed = 4;
                    character2.GetComponent<CharacterScript>().movementSpeed = 6;
                }
                else
                {
                    character1.GetComponent<CharacterScript>().movementSpeed = 2;
                    character2.GetComponent<CharacterScript>().canMove = false;
                }
            }
            else //char2
            {
                if (currentAbility == 0)//size change
                {
                    character1.GetComponent<CharacterScript>().movementSpeed = 3;
                    character1.GetComponent<CharacterScript>().movementSpeed = 7;
                }
                else
                {
                    character1.GetComponent<CharacterScript>().movementSpeed = 3;
                    character1.GetComponent<CharacterScript>().canMove = false;
                }
            }
        }
        else
        {
            character1.GetComponent<CharacterScript>().movementSpeed = 5;
            character2.GetComponent<CharacterScript>().movementSpeed = 5;
        }
    }

}
