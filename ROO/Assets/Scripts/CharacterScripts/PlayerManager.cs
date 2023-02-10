using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject character1, character2;
    [HideInInspector]
    public GameObject sizeC1, sizeC2;
    [HideInInspector]
    public CharacterScript currentCharacter;

    public int currentAbility, currentLevel, depth;
    public bool moveBoth, cutscenePlaying;

    public CharacterScript character1script, character2script, otherCharacter;
    public CinemachineVirtualCamera zoubooCam, koobouCam;
    public RespawnPoint respawnPoint;
    public PlayerControls playerControls;

    private GameManager gameManager;
    private MiddleBond middleBond;
    private Vector2 movement;

    [SerializeField]
    private bool floatAbilityActive, sizeAbilityActive, hasReachedMax;



    private void Awake()
    {
        Initialize();
        PlayerControlsGameplay();
        PlayerControlsUI();
    }

    private void Update()
    {
        //walking animation
        WalkingState();

        //see if the mac is reached
        CheckOutOfRange();

        //move character 
        if (currentCharacter.canMove && movement.x != 0 && !cutscenePlaying)
            Move();

        //update floating to follow
        if (floatAbilityActive)
            UpdateFloating();
    }



    /* Start */
    private void Initialize()
    {
        middleBond = FindObjectOfType<MiddleBond>();
        gameManager = FindObjectOfType<GameManager>();
        character1script = character1.GetComponent<CharacterScript>();
        character2script = character2.GetComponent<CharacterScript>();
        currentCharacter = character1script;
        otherCharacter = character2script;
        cutscenePlaying = false;

        SetMovementSpeed();
        SetWeight();

        if (currentLevel == 0)
            depth = 2;
        else
            depth = 1;
    }



    /* Movement */
    //call the character(s) to move
    private void Move()
    {
        //check which side you are
        SetLeftRight();
        currentCharacter.Move(movement.x);
        gameManager.PlaySound("walk" + currentCharacter.tag);
        if (moveBoth)
        {
            otherCharacter.Move(movement.x);
            gameManager.PlaySound("walk" + otherCharacter.tag);
        }
    }

    //set if the characters can move
    private void MoveTogether()
    {
        if (!floatAbilityActive && !sizeAbilityActive)
        {
            //set true if false false if true; 
            if (moveBoth)
                moveBoth = false;
            else
                moveBoth = true;

            SetCanMove();
            gameManager.UpdateConnection();
        }
        else
            gameManager.PlaySound("cantResize");
    }

    //set if the characters can move
    private void SetCanMove()
    {
        if (moveBoth)
        {
            character1script.canMove = true;
            character2script.canMove = true;
        }
        else
        {
            if (floatAbilityActive)
            {
                if (character1script.usedFloatAbility)
                {
                    character1script.canMove = true;
                    character2script.canMove = false;
                }
                else
                {
                    character1script.canMove = false;
                    character2script.canMove = true;
                }
            }
            else
            {
                if (currentCharacter == character1script)
                {
                    character1script.canMove = true;
                    character2script.canMove = false;
                }
                else
                {
                    character1script.canMove = false;
                    character2script.canMove = true;
                }
            }
        }
    }

    //walking behaviour when max reached
    private void MaxReached(bool r)
    {
        if (r)
        {
            //sizing ability active
            if (sizeAbilityActive)
            {
                //if they move away from eachother
                if ((currentCharacter.left && movement.x < 0) || (!currentCharacter.left && movement.x > 0))
                {
                    //if character 1 used the ability
                    if (currentCharacter.usedSizeAbility)
                    {
                        moveBoth = true;
                        currentCharacter.movementSpeed = 2;
                        otherCharacter.movementSpeed = 1;
                    }
                    // if char 2 used ability 
                    else
                    {
                        moveBoth = true;
                        currentCharacter.movementSpeed = 5;
                        otherCharacter.movementSpeed = 4;
                    }
                }
                //if they are walking towards eachother 
                else
                    moveBoth = false;

                currentCharacter.canMove = true;
            }
            //ability not active
            else
            {
                //if the character is left of the other character
                if (currentCharacter.left && movement.x > 0 || !currentCharacter.left && movement.x < 0)
                    currentCharacter.canMove = true;
                //if the character is right of the other character 
                else
                    currentCharacter.canMove = false;
            }
            hasReachedMax = true;
        }
        //if max is not reached
        else
        {
            if (hasReachedMax)
                moveBoth = false;
            currentCharacter.canMove = true;
            //reset movementspeed from above
            SetMovementSpeed();
            hasReachedMax = false;
        }

    }

    //set if the current character is left or right of the other character
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

    //call the MaxReached function with the right values
    private void CheckOutOfRange()
    {
        if (middleBond.outOfRange)
            MaxReached(true);
        else
            MaxReached(false);
    }



    /* Abilities */

    //sets if ability is used, and if it is used by which character
    private void SetUsedAbility(bool reset, int ability) //0 is size, 1 is float
    {
        // if ability needs to get reset 
        if (reset)
        {
            //size
            if (ability == 0)
            {
                sizeAbilityActive = false;
                character1script.usedSizeAbility = false;
                character2script.usedSizeAbility = false;
            }
            //float
            else
            {
                floatAbilityActive = false;
                character1script.usedFloatAbility = false;
                character2script.usedFloatAbility = false;
            }
            character1script.usedAbility = false;
            character2script.usedAbility = false;
        }
        else
        {
            currentCharacter.usedAbility = true;
            //size
            if (ability == 0)
            {
                sizeAbilityActive = true;

                //current character is 1
                if (currentCharacter == character1script)
                {
                    character1script.usedSizeAbility = true;
                    character2script.usedSizeAbility = false;
                    character2script.usedAbility = false;
                }
                //current character is 2
                else
                {
                    character1script.usedSizeAbility = false;
                    character2script.usedSizeAbility = true;
                    character1script.usedAbility = false;
                }
            }
            //float
            else
            {
                floatAbilityActive = true;
                //current character is 1
                if (currentCharacter == character1script)
                {
                    character1script.usedFloatAbility = true;
                    character2script.usedFloatAbility = false;
                    character2script.usedAbility = false;
                }
                //current character is 2
                else
                {
                    character1script.usedFloatAbility = false;
                    character2script.usedFloatAbility = true;
                    character1script.usedAbility = false;
                }
            }
        }

    }

    //sizing ability
    public void Sizing()
    {
        //if you already used ability reset, else check if u can resize depending on current character 
        if (currentCharacter.canResize || sizeAbilityActive)
        {
            //reset if character used the size ability again
            if (currentCharacter.usedSizeAbility || otherCharacter.usedSizeAbility)
            {
                ResetsSize();
                SetUsedAbility(true, 0);
                gameManager.UpdateMechanics(2, true);
                gameManager.PlaySound("sizeout");
            }
            //use the size ability
            else
            {
                UpdateSize();
                SetUsedAbility(false, 0);
                gameManager.PlaySound("sizein");
                gameManager.UpdateMechanics(2, false);
            }
        }
        else
            gameManager.PlaySound("cantResize");
    }

    //update to the right size
    public void UpdateSize()
    {
        if (currentCharacter == character1script)
        {
            sizeC1.transform.localScale *= 1.3f;
            sizeC2.transform.localScale /= 1.3f;
        }
        else
        {
            sizeC2.transform.localScale *= 1.5f;
            sizeC1.transform.localScale /= 1.5f;
        }
    }

    //go back to original size
    public void ResetsSize()
    {
        if (character1script.usedSizeAbility)
        {
            sizeC1.transform.localScale /= 1.3f;
            sizeC2.transform.localScale *= 1.3f;
        }
        else
        {
            sizeC1.transform.localScale *= 1.5f;
            sizeC2.transform.localScale /= 1.5f;
        }
    }

    //float ability 
    public void Floating()
    {
        //if the float ability is already active, reset it 
        if (floatAbilityActive)
        {
            ResetFloating();
            SetUsedAbility(true, 1);
            gameManager.PlaySound("floatout");
            gameManager.UpdateMechanics(2, true);
        }
        // if character that needs to get flying is on the ground and you used the floating ability when it's not yet active
        else if (otherCharacter.isGrounded)
        {
            SetUsedAbility(false, 1);
            UpdateFloating();
            gameManager.PlaySound("floatin");
            gameManager.UpdateMechanics(2, false);
        }
    }

    //follow current floating position
    private void UpdateFloating()
    {
        if (currentCharacter.usedFloatAbility)
            otherCharacter.MoveTowardsPlace(currentCharacter.floatCheck.transform);
        else
            currentCharacter.MoveTowardsPlace(otherCharacter.floatCheck.transform);
    }

    //reset floating (stopping it at the position of trigger)
    private void ResetFloating()
    {
        if (currentCharacter.usedFloatAbility)
            otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, depth);
        else if (otherCharacter.usedFloatAbility)
            currentCharacter.transform.position = new Vector3(currentCharacter.transform.position.x, currentCharacter.transform.position.y, -depth);
    }




    /* Values */

    //set movementspeed
    public void SetMovementSpeed()
    {
        //float ability active 
        if (floatAbilityActive)
        {
            if (character1script.usedFloatAbility)
                character1script.movementSpeed = 2;
            else
                character2script.movementSpeed = 3;
            SetCanMove();
        }

        //size ability active
        else if (sizeAbilityActive)
        {
            if (character1script.usedSizeAbility)
            {
                character1script.movementSpeed = 4;
                character2script.movementSpeed = 6;
                //character1script.canWalkOnWater = false;
            }
            else
            {
                character2script.movementSpeed = 3;
                character1script.movementSpeed = 7;
                //character1script.canWalkOnWater = true;
            }
        }

        //no ability active
        else
        {
            character1script.movementSpeed = 5;
            character2script.movementSpeed = 5;
            //character1script.canWalkOnWater = false;
        }
    }

    //set weight
    public void SetWeight()
    {
        //float ability active 
        if (floatAbilityActive)
        {
            if (character1script.usedFloatAbility)
            {
                character1script.weight = 6;
                character2script.weight = 0;
            }
            else
            {
                character1script.weight = 0;
                character2script.weight = 6;
            }
        }

        //size ability active
        else if (sizeAbilityActive)
        {
            if (character1script.usedSizeAbility)
            {
                character1script.weight = 4;
                character2script.weight = 2;
            }
            else
            {
                character1script.weight = 1;
                character2script.weight = 5;
            }
        }

        //no ability active
        else
        {
            character1script.weight = 2;
            character2script.weight = 4;
        }
    }

    //set gravity
    public void SetGravity()
    {
        //float ability active 
        if (floatAbilityActive)
        {
            if (character1script.usedFloatAbility)
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

        //gravity for anything else 
        else
        {
            character1script.rb.useGravity = true;
            character2script.rb.useGravity = true;
        }
    }



    /* Characters */

    //respawn character at respawn point
    public void RespawnCharacters()
    {
        if (respawnPoint != null)
        {
            //change position to respain point
            if (currentCharacter == character1script)
            {
                character1.transform.position = new Vector3(respawnPoint.spawnPoints[0].transform.position.x, respawnPoint.spawnPoints[0].transform.position.y, 0);
                character2.transform.position = new Vector3(respawnPoint.spawnPoints[1].transform.position.x, respawnPoint.spawnPoints[1].transform.position.y, depth);
            }
            else
            {
                character1.transform.position = new Vector3(respawnPoint.spawnPoints[0].transform.position.x, respawnPoint.spawnPoints[0].transform.position.y, depth);
                character2.transform.position = new Vector3(respawnPoint.spawnPoints[1].transform.position.x, respawnPoint.spawnPoints[1].transform.position.y, 0);
            }
        }
        //animations
        gameManager.UpdateMechanics(2, true);
        gameManager.SetWalking(false);

        //reset ability if active
        ResetAbility();
    }

    //switch characters, move, animations, camera, position and eye 
    public void SwitchCharacter()
    {
        //if current character is character 1
        if (currentCharacter == character1script)
        {
            //make the active character able to move
            character1script.canMove = false;
            character2script.canMove = true;
            //right depth for active character
            character1.transform.position = new Vector3(character1.transform.position.x, character1.transform.position.y, depth);
            character2.transform.position = new Vector3(character2.transform.position.x, character2.transform.position.y, -depth);
            //change characterscripts
            otherCharacter = character1script;
            currentCharacter = character2script;
            //change active cam
            zoubooCam.Priority = 0;
            koobouCam.Priority = 1;
            //change active eye 
            character1script.EyePoint.GetComponent<MeshRenderer>().enabled = false;
            character2script.EyePoint.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            //make the active character able to move
            character2script.canMove = false;
            character1script.canMove = true;
            //right depth for active character
            character1.transform.position = new Vector3(character1.transform.position.x, character1.transform.position.y, -depth);
            character2.transform.position = new Vector3(character2.transform.position.x, character2.transform.position.y, depth);
            //change characterscripts
            otherCharacter = character2script;
            currentCharacter = character1script;
            //change active cam
            zoubooCam.Priority = 1;
            koobouCam.Priority = 0;
            //change active eye 
            character1script.EyePoint.GetComponent<MeshRenderer>().enabled = true;
            character2script.EyePoint.GetComponent<MeshRenderer>().enabled = false;
        }
        SetCanMove();
        //sound and animation
        gameManager.PlaySound("swap");
        gameManager.UpdateMechanics(1, false);
        gameManager.UpdateConnection();
    }



    /* Ability calls */

    //activate the ability
    public void TriggerAbility()
    {
        //disable move together if triggering ability 
        if (moveBoth)
        {
            moveBoth = false;
            gameManager.UpdateConnection();
        }

        switch (currentAbility)
        {
            // size
            case 0:
                Sizing();
                break;
            // float
            case 1:
                Floating();
                break;
        }
        SetGravity();
        SetWeight();
        SetMovementSpeed();

    }

    //reset abilities
    private void ResetAbility()
    {
        if (sizeAbilityActive)
            Sizing();
        else if (floatAbilityActive)
            Floating();

        SetGravity();
        SetWeight();
        SetMovementSpeed();
    }

    //switching of abilities
    public void SwitchAbility()
    {
        //if ability is active
        if (sizeAbilityActive || floatAbilityActive)
        {
            ResetAbility();

            //deactivate trigger ability ani
            gameManager.UpdateMechanics(2, true);
        }

        //size
        if (currentAbility == 0)
            currentAbility = 1;
        //float
        else
            currentAbility = 0;

        //switch ability ani 
        gameManager.UpdateMechanics(0, false);
    }



    /* GameManager Calls */

    //call for pause to the game manager
    public void DoPause()
    {
        gameManager.Pause();
    }

    //give game manager info on walking 
    private void WalkingState()
    {
        if (movement.x != 0)
            gameManager.SetWalking(true);
        else
            gameManager.SetWalking(false);
    }



    /* Player Controls */

    //UI playercontrols
    private void PlayerControlsUI()
    {
        playerControls.UI.Back.performed += ctx => gameManager.GoBack();
        playerControls.UI.Click.performed += ctx => gameManager.ClickButton();
        playerControls.UI.Navigate.performed += ctx => gameManager.GetCurrentButton();
        playerControls.UI.Navigate.performed += ctx => gameManager.SetValue(ctx.ReadValue<Vector2>());
    }

    //Gameplay playercontrols
    private void PlayerControlsGameplay()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>(); //lamda expression to preform function
        playerControls.Gameplay.SwitchCharacter.performed += ctx => SwitchCharacter();
        playerControls.Gameplay.SwitchAbility.performed += ctx => SwitchAbility();
        playerControls.Gameplay.TriggerAbility.performed += ctx => TriggerAbility();
        playerControls.Gameplay.ToggleButton.performed += ctx => DoToggleLever();
        playerControls.Gameplay.Respawn.performed += ctx => RespawnCharacters();
        playerControls.Gameplay.Grab.performed += ctx => DoGrab();
        playerControls.Gameplay.MoveTogether.performed += ctx => MoveTogether();
        playerControls.Gameplay.Pause.performed += ctx => DoPause();
        playerControls.Gameplay.Interact.performed += ctx => DoInteract();
        playerControls.Gameplay.Enable();
    }

    // Enable/disable playercontrols depending on if the game is paused
    public void EnablePlayerControls(bool paused)
    {
        if (paused)
        {
            playerControls.Gameplay.Disable();
            playerControls.UI.Enable();
        }
        else
        {
            playerControls.Gameplay.Enable();
            playerControls.UI.Disable();
        }
    }

    // Enable/disable playercontrols depending on if the game is at eshus lair
    public void EnableEshuControls(bool paused)
    {
        if (paused)
        {
            playerControls.Gameplay.Disable();
            playerControls.UI.Enable();
        }
        else
        {
            playerControls.Gameplay.Enable();
            playerControls.Gameplay.MoveTogether.Disable();
            playerControls.Gameplay.TriggerAbility.Disable();
            playerControls.Gameplay.SwitchAbility.Disable();
            playerControls.Gameplay.Grab.Disable();
            playerControls.UI.Disable();
        }
    }

    //playercontrols for tutorial only 
    public void SetTutorialControls(int stage)
    {
        switch (stage)
        {
            case 1:
                playerControls.Gameplay.MoveTogether.Enable();
                break;
            case 2:
                playerControls.Gameplay.TriggerAbility.Enable();
                break;
            case 3:
                playerControls.Gameplay.SwitchAbility.Enable();
                middleBond.maxDistance = 20;
                break;
            case 4:
                playerControls.Gameplay.Grab.Enable();
                break;
            default:
                playerControls.Gameplay.MoveTogether.Disable();
                playerControls.Gameplay.TriggerAbility.Disable();
                playerControls.Gameplay.SwitchAbility.Disable();
                playerControls.Gameplay.Grab.Disable();
                break;
        }
    }



    /* Character Calls */

    //call this character to grab
    private void DoGrab()
    {
        currentCharacter.GrabObject();
    }

    //call this character to interact
    private void DoInteract()
    {
        currentCharacter.InteractWithObject();
    }



    /* Unused */

    //call for lever toggle 
    public void DoToggleLever()
    {
        currentCharacter.ToggleLever();
    }
}
