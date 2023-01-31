using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject character1, character2, sizeC1, sizeC2;
    [HideInInspector]
    public CharacterScript currentCharacter;


    public int currentAbility, currentLevel, depth;

    public CharacterScript character1script, character2script, otherCharacter;
    private CinemachineBrain cmBrain;

    [SerializeField]
    public CinemachineVirtualCamera zoubooCam, koobouCam;

    private GameManager gameManager;
    public PlayerControls playerControls;
    private MiddleBond middleBond;
    private Vector2 movement;

    public RespawnPoint respawnPoint;
    [SerializeField]
    private bool abilityActive, hasReachedMax;
    public bool moveBoth;

    public bool cutscenePlaying = false;

    private void Awake()
    {
        Initialize();
        PlayerControlsGameplay();
        PlayerControlsUI();
    }
    private void Update()
    {

        if (middleBond.outOfRange)
            MaxReached(true);
        else
            MaxReached(false);

        if (currentCharacter.canMove && movement.x != 0 && !cutscenePlaying)
            Move();
  
        if (currentAbility == 1 && abilityActive)
            UpdateFloating();
    }

    private void Initialize()
    {
        cmBrain = FindObjectOfType<CinemachineBrain>();
        middleBond = FindObjectOfType<MiddleBond>();
        gameManager = FindObjectOfType<GameManager>();
        character1script = character1.GetComponent<CharacterScript>();
        character2script = character2.GetComponent<CharacterScript>();
        currentCharacter = character1script;
        otherCharacter = character2script;
        abilityActive = false;
        SetMovementSpeed();
        SetWeight();


        if (currentLevel == 0)
            depth = 2;
        else
            depth = 1;
    }
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
        playerControls.Gameplay.Enable();
    }
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
    private void MaxReached(bool r)
    {
        if (r)
        {
            SetLeftRight();
            if (currentAbility == 0 && abilityActive) //sizing ability active
            {

                if ((currentCharacter.left && movement.x < 0) || (!currentCharacter.left && movement.x > 0)) //if they move away from eachother
                {
                    if (currentCharacter.usedAbility) //if character 1 used the ability
                    {
                        moveBoth = true;
                        currentCharacter.movementSpeed = 2;
                        otherCharacter.movementSpeed = 1;
                    }
                    else // if char 2 used ability 
                    {
                        moveBoth = true;
                        currentCharacter.movementSpeed = 5;
                        otherCharacter.movementSpeed = 4;
                    }
                }
                else //if they are walking towards eachother 
                    moveBoth = false;

                currentCharacter.canMove = true;
            }
            else //ability not active
            {
                if (currentCharacter.left && movement.x > 0 || !currentCharacter.left && movement.x < 0) //if the character is left of the other character
                    currentCharacter.canMove = true;
                else //if the character is right of the other character 
                    currentCharacter.canMove = false;
            }
            hasReachedMax = true;
        }
        else
        {
            if (hasReachedMax)
                moveBoth = false;
            currentCharacter.canMove = true;
            SetMovementSpeed();
            hasReachedMax = false;
        }

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
    private void Move()
    {
        currentCharacter.Move(movement.x);
        gameManager.PlaySound("walk" + currentCharacter.tag);
        if (moveBoth)
        {
            otherCharacter.Move(movement.x);
            gameManager.PlaySound("walk" + otherCharacter.tag);
        }
    }
    private void MoveTogether()
    {
        if (!abilityActive)
        {
            SetMovementSpeed();
            if (moveBoth)
            {
                if (currentCharacter == character1script)
                {
                    gameManager.UpdateConnection(1);
                    character1script.canMove = true;
                    character2script.canMove = false;
                }
                else
                {
                    gameManager.UpdateConnection(2);
                    character1script.canMove = false;
                    character2script.canMove = true;
                }
                moveBoth = false;
            }
            else
            {
                gameManager.UpdateConnection(0);
                character1script.canMove = true;
                character2script.canMove = true;
                moveBoth = true;
            }
        }//feedback cant move together rn 
    }
    private void UpdateFloating()
    {
        if (currentCharacter.usedAbility)
            otherCharacter.MoveTowardsPlace(currentCharacter.floatCheck.transform);
        else
            currentCharacter.MoveTowardsPlace(otherCharacter.floatCheck.transform);
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
        if (abilityActive)
        {
            if (currentCharacter.usedAbility)
            {
                otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, depth);
                currentCharacter.usedAbility = false;

            }
            else
            {
                currentCharacter.transform.position = new Vector3(currentCharacter.transform.position.x, currentCharacter.transform.position.y, -depth);
                otherCharacter.usedAbility = false;
            }
            abilityActive = false;
            gameManager.PlaySound("floatout");
            gameManager.UpdateMechanics(2, true);
        }
        else if (otherCharacter.isGrounded)
        {
            abilityActive = true;
            SetUsedAbility();
            gameManager.PlaySound("floatin");
            otherCharacter.MoveTowardsPlace(currentCharacter.floatCheck.transform);
            gameManager.UpdateMechanics(2, false);
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
                gameManager.UpdateMechanics(2, true);
                gameManager.PlaySound("sizeout");
            }
            else
            {
                abilityActive = true;
                SetUsedAbility();
                SetSize();
                gameManager.PlaySound("sizein");
                gameManager.UpdateMechanics(2, false);
            }
        }
        else
            gameManager.PlaySound("cantResize");
    }
    private void DoGrab()
    {
        currentCharacter.GrabObject();
    }
    public void DefaultValuesSize()
    {
        if (character1script.usedAbility)
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
    public void SetMovementSpeed()
    {
        if (currentCharacter == character1script)
            character1script.canMove = true;
        else
            character2script.canMove = true;

        if (abilityActive)
        {
            if (character1script.usedAbility) //character1script
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
            if (character1script.usedAbility) //character1script
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
            sizeC1.transform.localScale *= 1.3f;
            sizeC2.transform.localScale /= 1.3f;
        }
        else
        {
            sizeC2.transform.localScale *= 1.5f;
            sizeC1.transform.localScale /= 1.5f;
        }
    }
    public void SetGravity()
    {
        if (currentAbility == 1 && abilityActive)
        {
            if (character1script.usedAbility)
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

            gameManager.UpdateMechanics(2, true);
            SetMovementSpeed();
            if (currentAbility == 0 && abilityActive)
                DefaultValuesSize();
            else
                SetGravity();
            character1script.usedAbility = false;
            character2script.usedAbility = false;
            abilityActive = false;
        }
    }
    public void SwitchCharacter()
    {
        if (currentCharacter == character1script)
        {
            character1script.canMove = false;
            character2script.canMove = true;
            character1.transform.position = new Vector3(character1.transform.position.x, character1.transform.position.y, depth);
            character2.transform.position = new Vector3(character2.transform.position.x, character2.transform.position.y, -depth);
            otherCharacter = character1script;
            currentCharacter = character2script;

            zoubooCam.Priority = 0;
            koobouCam.Priority = 1;
            character1script.EyePoint.GetComponent<MeshRenderer>().enabled = false;
            character2script.EyePoint.GetComponent<MeshRenderer>().enabled = true;
            gameManager.UpdateConnection(2);
        }
        else
        {
            character2script.canMove = false;
            character1script.canMove = true;
            character1.transform.position = new Vector3(character1.transform.position.x, character1.transform.position.y, -depth);
            character2.transform.position = new Vector3(character2.transform.position.x, character2.transform.position.y, depth);
            otherCharacter = character2script;
            currentCharacter = character1script;
            zoubooCam.Priority = 1;
            koobouCam.Priority = 0;
            character1script.EyePoint.GetComponent<MeshRenderer>().enabled = true;
            character2script.EyePoint.GetComponent<MeshRenderer>().enabled = false;
            gameManager.UpdateConnection(1);
        }
        SetMovementSpeed();
        gameManager.PlaySound("swap");
        gameManager.UpdateMechanics(1, false);
    }
    public void DoToggleLever()
    {
        currentCharacter.ToggleLever();
    }
    public void TriggerAbility()
    {
        if (moveBoth) //disable move together if triggering ability 
        {
            moveBoth = false;
            if (currentCharacter == character1script)
                gameManager.UpdateConnection(1);
            else
                gameManager.UpdateConnection(2);

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
    public void SwitchAbility()
    {
        if (abilityActive)
        {
            TriggerAbility();
            abilityActive = false;
            gameManager.UpdateMechanics(2, true);//deactivate trigger ability ani
        }

        if (currentAbility == 0)//size
            currentAbility = 1;
        else //float
            currentAbility = 0;

        gameManager.UpdateMechanics(0, false); //switch ability ani 
    }
    public void DoPause()
    {
        gameManager.Pause();
    }
    private void PlayerControlsUI()
    {
        playerControls.UI.Back.performed += ctx => gameManager.GoBack();
        playerControls.UI.Click.performed += ctx => gameManager.ClickButton();
        playerControls.UI.Navigate.performed += ctx => gameManager.GetCurrentButton();
        playerControls.UI.Navigate.performed += ctx => gameManager.SetValue(ctx.ReadValue<Vector2>());
    }
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
}
