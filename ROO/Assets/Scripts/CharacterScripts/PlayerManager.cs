using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject character1, character2;

    private CharacterScript character1script;
    private CharacterScript character2script;

    [HideInInspector]
    public CharacterScript currentCharacter;
    private CharacterScript otherCharacter;

    public RespawnPoint respawnPoint;

    private Vector2 movement;

    private CameraScript camScript;
    private PlayerControls playerControls;

    [SerializeField]
    private float maxDistanceBetweenPlayers;

    private MiddleBond middleBond;

    [HideInInspector]
    public int currentAbility;

    [SerializeField]
    private bool abilityActive, moveBoth, hasReachedMax;

    private void Awake()
    {
        camScript = FindObjectOfType<CameraScript>();
        middleBond = FindObjectOfType<MiddleBond>();
        character1script = character1.GetComponent<CharacterScript>();
        character2script = character2.GetComponent<CharacterScript>();
        currentCharacter = character1script;
        otherCharacter = character2script;
        abilityActive = false;
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>(); //lamda expression to preform function
        playerControls.Gameplay.SwitchCharacter.performed += ctx => SwitchCharacter();
        playerControls.Gameplay.SwitchAbility.performed += ctx => SwitchAbility();
        playerControls.Gameplay.TriggerAbility.performed += ctx => TriggerAbility();
        playerControls.Gameplay.ToggleButton.performed += ctx => DoToggleLever();
        playerControls.Gameplay.Respawn.performed += ctx => RespawnCharacters();
        playerControls.Gameplay.Grab.performed += ctx => DoGrab();
        playerControls.Gameplay.MoveTogether.performed += ctx => MoveTogether();
        playerControls.Gameplay.Enable();
    }
    private void Update()
    {
        if (middleBond.outOfRange)
            MaxReached(true);
        else if (hasReachedMax)
            MaxReached(false);

        if (currentCharacter.canMove)
            Move();

        if (currentAbility == 1 && abilityActive)
            UpdateFloating();
    }
    private void MoveTogether()
    {
        if (!moveBoth)
            moveBoth = true;
        else
            moveBoth = false;
    }
    private void MaxReached(bool r)
    {
        if (r)
        {
            hasReachedMax = true;
            SetLeftRight();
            if (currentAbility == 0 && abilityActive) //sizing ability active
            {

                if ((currentCharacter.left && movement.x < 0) || (!currentCharacter.left && movement.x > 0)) //if they move away from eachother
                {
                    if (currentCharacter.usedAbility) //if character 1 used the ability
                    {
                        currentCharacter.canMove = true;
                        moveBoth = true;
                        currentCharacter.movementSpeed = 2;
                        otherCharacter.movementSpeed = 1;
                    }
                    else // if char 2 used ability 
                    {
                        currentCharacter.canMove = true;
                        moveBoth = true;
                        currentCharacter.movementSpeed = 5;
                        otherCharacter.movementSpeed = 4;
                    }
                }
                else //if they are walking towards eachother 
                {
                    moveBoth = false;
                    currentCharacter.canMove = true;
                }
            }
            else //ability not active
            {
                if (currentCharacter.left && movement.x > 0 || !currentCharacter.left && movement.x < 0) //if the character is left of the other character
                    currentCharacter.canMove = true;
                else //if the character is right of the other character 
                    currentCharacter.canMove = false;
            }
        }
        else
        {
            hasReachedMax = false;
            moveBoth = false;
            currentCharacter.canMove = true;
        }

    }
    private void Move()
    {
        currentCharacter.Move(movement.x);
        if (moveBoth)
            otherCharacter.Move(movement.x);
    }
    private void UpdateFloating()
    {
        if (currentCharacter.usedAbility)
            otherCharacter.MoveTowardsPlace(currentCharacter.floatCheck.transform);
        else
            currentCharacter.MoveTowardsPlace(otherCharacter.floatCheck.transform);
    }
    private void SetLeftRight()
    {
        if (character1.transform.position.x > character2.transform.position.x)
        {
            character1script.left = false;
            character2script.left = true;
        }
        else
        {
            character1script.left = true;
            character2script.left = false;
        }
    }
    private void DoGrab()
    {
        currentCharacter.GrabObject();
    }
    private void SetUsedAbility()
    {
        currentCharacter.usedAbility = true;
        if (currentCharacter == character1script)
            character2script.usedAbility = false;
        else
            character1script.usedAbility = false;
    }
    public void Floating()
    {
        currentCharacter.usedAbility = true;
        if (abilityActive)
        {
            abilityActive = false;
            currentCharacter.usedAbility = false;
            SetGravity();
        }
        else if (otherCharacter.isGrounded)
        {
            abilityActive = true;
            SetUsedAbility();
            otherCharacter.MoveTowardsPlace(currentCharacter.floatCheck.transform);
        }
    }
    public void Sizing()
    {

        if (currentCharacter.canResize || currentCharacter.usedAbility)//todo checker for mini and make size table 
        {
            if (abilityActive)
            {
                DefaultValuesSize();
                abilityActive = false;
                currentCharacter.usedAbility = false;
            }
            else
            {
                abilityActive = true;
                SetUsedAbility();
                SetSize();
                SetGravity();
            }

        }
    }
    public void DefaultValuesSize()
    {
        if (character1script.usedAbility)
        {
            character1.transform.localScale /= 1.3f;
            character2.transform.localScale *= 1.3f;
        }
        else
        {
            character1.transform.localScale *= 1.5f;
            character2.transform.localScale /= 1.5f;
        }
    }
    public void SetMovementSpeed()
    {
        character1script.canMove = true;
        character2script.canMove = true;
        if (abilityActive)
        {
            if (currentCharacter == character1script) //character1script
            {
                if (currentAbility == 0)//size change
                {

                    character1script.movementSpeed = 4;
                    character2script.movementSpeed = 6;
                    character1script.canWalkOnWater = false;

                }
                else
                {
                    character1script.movementSpeed = 2;
                    character2script.canMove = false;
                    character1script.canWalkOnWater = false;
                }
            }
            else //character2script
            {
                if (currentAbility == 0)//size change
                {
                    character2script.movementSpeed = 3;
                    character1script.movementSpeed = 7;
                    character1script.canWalkOnWater = true;
                }
                else
                {
                    character2script.movementSpeed = 3;
                    character1script.canMove = false;
                    character1script.canWalkOnWater = false;
                }
            }
        }
        else
        {
            character1script.movementSpeed = 5;
            character2script.movementSpeed = 5;
            character1script.canWalkOnWater = false;
        }
    }
    public void SetWeight()
    {
        if (abilityActive)
        {
            if (currentCharacter == character1script) //character1script
            {
                if (currentAbility == 0)//size change
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
            else //character2script
            {
                if (currentAbility == 0)//size change
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
    public void SetSize()
    {
        if (currentCharacter == character1script)
        {
            character1.transform.localScale *= 1.3f;
            character2.transform.localScale /= 1.3f;
        }
        else
        {
            character2.transform.localScale *= 1.5f;
            character1.transform.localScale /= 1.5f;
        }
    }
    public void SetGravity()
    {
        if (currentAbility == 1 && abilityActive)
        {
            if (currentCharacter == character1script)
            {
                character1script.rb.useGravity = true;
                character2script.rb.useGravity = false;
            }
            else
            {
                character1script.rb.useGravity = false;
                character2script.rb.useGravity = true;
            }
        }
        else
        {
            character1script.rb.useGravity = true;
            character2script.rb.useGravity = true;
        }

    }
    public void RespawnCharacters()
    {
        if (respawnPoint != null)
        {
            character1.GetComponentInChildren<Collider>().isTrigger = false;
            character2.GetComponentInChildren<Collider>().isTrigger = false;
            character1.transform.position = respawnPoint.spawnPoints[0].transform.position;
            character2.transform.position = respawnPoint.spawnPoints[1].transform.position;
            abilityActive = false;
            SetGravity();
            character1script.usedAbility = false;
            character2script.usedAbility = false;
        }
    }
    public void SwitchCharacter()
    {
        if (currentCharacter == character1script)
        {
            character1script.canMove = false;
            character2script.canMove = true;
            otherCharacter = currentCharacter;
            currentCharacter = character2script;
            camScript.player = character2;
            character1script.EyePoint.GetComponent<MeshRenderer>().enabled = false;
            character2script.EyePoint.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            character2script.canMove = false;
            character1script.canMove = true;
            otherCharacter = currentCharacter;
            currentCharacter = character1script;
            camScript.player = character1;
            character1script.EyePoint.GetComponent<MeshRenderer>().enabled = true;
            character2script.EyePoint.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void DoToggleLever()
    {
        currentCharacter.ToggleLever();
    }
    public void TriggerAbility()
    {
        currentCharacter.canMove = true;
        switch (currentAbility)
        {
            // size
            case 0:
                Sizing();
                break;
            // float
            case 1:
                Floating();
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
                Sizing();
                SetGravity();
                abilityActive = false;
            }
            currentAbility = 1;
        }
        else
        {  //float
            if (abilityActive)
            {
                Floating();
                abilityActive = false;
                SetMovementSpeed();
            }
            currentAbility = 0;
        }
    }
}
