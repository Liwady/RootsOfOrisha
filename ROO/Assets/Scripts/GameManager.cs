using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit, amountOfEyes, currentAbility;
    public bool abilityActive;
    public TMP_Text fruitText, eyesText, abilityText;
    public GameObject character1, character2;
    public CharacterScript currentChar, otherChar, character1script, character2script;
    public RespawnPoint respawnPoint;
    public Camera mainCamera;

    private void Awake()
    {
        amountOfFruit = 0;
        character1script = character1.GetComponent<CharacterScript>();
        character2script = character2.GetComponent<CharacterScript>();
        currentChar = character1script;
        otherChar = character2script;
        abilityActive = false;
        currentAbility = 0;
        abilityText.text = currentAbility.ToString();
    }
    private void Update()
    {
        abilityText.text = currentAbility.ToString();
    }
    public void RespawnCharacters()
    {
        if (respawnPoint != null)
        {
            character1.transform.position = respawnPoint.spawnPoints[0].transform.position;
            character2.transform.position = respawnPoint.spawnPoints[1].transform.position;
        }
    }
    public void SwitchCharacter()
    {
        if (currentChar.gameObject == character1)
        {
            character1script.enabled = false;
            character2script.enabled = true;
            otherChar = currentChar;
            currentChar = character2script;
            mainCamera.GetComponent<CameraScript>().player = character2;
        }
        else
        {
            character2script.enabled = false;
            character1script.enabled = true;
            otherChar = currentChar;
            currentChar = character1script;
            mainCamera.GetComponent<CameraScript>().player = character1;
        }
    }
    public void ToggleLever()
    {
        if (currentChar.inRangeLever != null)
            currentChar.inRangeLever.ToggleLever();
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
                SetMovementSpeed();
            }
            currentAbility = 0;
        }
    }
    public void SetSize()
    {
        if (currentChar.gameObject == character1)
        {
            character1.transform.localScale *= 1.8f;
            character2.transform.localScale /= 1.8f;
        }
        else
        {
            character2.transform.localScale *= 1.8f;
            character1.transform.localScale /= 1.8f;
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
                    character1script.weight = 4;
                    character2script.weight = 2;
                }
                else
                {
                    character1script.weight = 6;
                    character2script.weight = 0;
                }
            }
            else //char2
            {
                if (currentAbility == 1)//size change
                {
                    character2script.weight = 5;
                    character1script.weight = 1;
                }
                else
                {
                    character2script.weight = 6;
                    character1script.weight = 0;
                }
            }
        }
        else
        {
            character1script.weight = 2;
            character2script.weight = 4;
        }
    }
    public void SetGravity()
    {
        if (currentAbility == 1 && abilityActive)
        {
            if (currentChar.gameObject == character1)
            {
                character1script.GetComponent<Rigidbody>().useGravity = true;
                character2script.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                character1script.GetComponent<Rigidbody>().useGravity = false;
                character2script.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
            character1script.GetComponent<Rigidbody>().useGravity = true;
            character2script.GetComponent<Rigidbody>().useGravity = true;
        }

    }
    public void SetMovementSpeed()
    {
        var char1 = character1script;
        var char2 = character2script;
        char1.canMove = true;
        char2.canMove = true;
        if (abilityActive)
        {
            if (currentChar.gameObject == character1) //char1
            {
                if (currentAbility == 0)//size change
                {
                    char1.movementSpeed = 4;
                    char2.movementSpeed = 6;
                    char1.canWalkOnWater = false;
                }
                else
                {
                    char1.movementSpeed = 2;
                    char2.canMove = false;
                    char1.canWalkOnWater = false;
                }
            }
            else //char2
            {
                if (currentAbility == 0)//size change
                {
                    char2.movementSpeed = 3;
                    char1.movementSpeed = 7;
                    char1.canWalkOnWater = true;
                }
                else
                {
                    char2.movementSpeed = 3;
                    char1.canMove = false;
                    char1.canWalkOnWater = false;
                }
            }
        }
        else
        {
            char1.movementSpeed = 5;
            char2.movementSpeed = 5;
            char1.canWalkOnWater = false;
        }
    }
}
