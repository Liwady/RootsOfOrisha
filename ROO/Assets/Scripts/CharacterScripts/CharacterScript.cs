using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float movementSpeed;
    public bool canResize;
    public Rigidbody rb;
    private Vector2 movement;
    private PlayerControls playerControls;
    private GameManager gameManager;
    public GameObject checker;
    public GameObject floatCheck;
    private bool dead, canGrab, canMoveObject;
    private bool activatedFloat;
    public int size;
    public int weight;
    WaterState statusW;

    private enum WaterState
    {
        CanWalk,
        Sink,
        Die
    }


    void Awake()
    {
        activatedFloat = false;
        statusW = WaterState.CanWalk;
        canResize = true;
        rb = GetComponentInChildren<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        SetWeight();
        SetMovementSpeed();
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>(); //lamda expression to preform function
        playerControls.Gameplay.SwitchCharacter.performed += ctx => gameManager.SwitchCharacter();
        playerControls.Gameplay.SwitchAbility.performed += ctx => gameManager.SwitchAbility();
        playerControls.Gameplay.TriggerAbility.performed += ctx => TriggerAbility();
    }
    private void Update()
    {
        Move();
        if (gameManager.currentAbility == 1 && activatedFloat)
            Floating();
    }
    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }
    private void Move()
    {
        rb.velocity = new Vector3(movement.x * movementSpeed, rb.velocity.y, rb.velocity.z);
    }
    public void TriggerAbility()
    {
        SetGravity();
        switch (gameManager.currentAbility)
        {
            //ability 1, gravity 
            case 1:
                Floating();
                break;
            //ability 2: size
            case 0:
                if (canResize)
                    SetSize();
                //else feedback that u cant 
                break;
        }
        SetWeight();
        SetMovementSpeed();
    }
    private void Floating()
    {
        activatedFloat = true;
        if (gameManager.abilityActive)
        {
            gameManager.character1.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
            gameManager.character2.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            if (gameManager.currentChar == gameManager.character1)
                gameManager.character2.transform.position = Vector3.MoveTowards(gameManager.character2.transform.position, floatCheck.transform.position, 3);
            else
                gameManager.character1.transform.position = Vector3.MoveTowards(gameManager.character2.transform.position, floatCheck.transform.position, 3);
        }
    }
    private void SetSize()
    {
        activatedFloat = false;
        if (checker.GetComponent<CheckerScript>().checkIfColliderEmpty())//todo checker for mini and make size table 
        {
            if (gameManager.abilityActive)
            {
                gameManager.character1.transform.localScale = new Vector3(0.9f, 0.4f, 1);
                gameManager.character2.transform.localScale = new Vector3(0.8f, 1.6f, 1);
                gameManager.abilityActive = false;
            }
            else
            {
                if (gameManager.currentChar == gameManager.character1)
                {
                    gameManager.character1.transform.localScale = gameManager.character1.transform.localScale * 1.8f;
                    gameManager.character2.transform.localScale = gameManager.character2.transform.localScale / 1.8f;
                }
                else
                {
                    gameManager.character2.transform.localScale = gameManager.character2.transform.localScale * 1.2f;
                    gameManager.character1.transform.localScale = gameManager.character1.transform.localScale / 1.2f;
                }
                gameManager.abilityActive = true;
            }
        }
    }
    private void SetWeight()
    {
        if (gameManager.abilityActive)
        {
            if (gameManager.currentChar == gameManager.character1) //char1
            {
                if (gameManager.currentAbility == 1)//size change
                {
                    gameManager.character1.GetComponent<CharacterScript>().weight = 5;
                    gameManager.character2.GetComponent<CharacterScript>().weight = 1;
                }
                else
                {
                    gameManager.character1.GetComponent<CharacterScript>().weight = 6;
                    gameManager.character2.GetComponent<CharacterScript>().weight = 0;
                }
            }
            else //char2
            {
                if (gameManager.currentAbility == 1)//size change
                {
                    gameManager.character2.GetComponent<CharacterScript>().weight = 4;
                    gameManager.character1.GetComponent<CharacterScript>().weight = 2;
                }
                else
                {
                    gameManager.character2.GetComponent<CharacterScript>().weight = 6;
                    gameManager.character1.GetComponent<CharacterScript>().weight = 0;
                }
            }
        }
        else
        {
            gameManager.character1.GetComponent<CharacterScript>().weight = 4;
            gameManager.character2.GetComponent<CharacterScript>().weight = 2;
        }
    }
    private void SetGravity()
    {
        if (gameManager.currentAbility == 1)
        {
            if (gameManager.currentChar == gameManager.character1)
            {
                gameManager.character1.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
                gameManager.character2.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                gameManager.character1.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = false;
                gameManager.character2.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
            gameManager.character1.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
            gameManager.character2.GetComponent<CharacterScript>().GetComponent<Rigidbody>().useGravity = true;
        }

    }
    private void SetMovementSpeed()
    {
        if (gameManager.abilityActive)
        {
            if (gameManager.currentChar == gameManager.character1) //char1
            {
                if (gameManager.currentAbility == 0)//size change
                {
                    movementSpeed = 4;
                    gameManager.character2.GetComponent<CharacterScript>().movementSpeed = 6;
                }
                else
                {
                    movementSpeed = 2;
                    gameManager.character2.GetComponent<CharacterScript>().movementSpeed = 0;
                }
            }
            else //char2
            {
                if (gameManager.currentAbility == 0)//size change
                {
                    movementSpeed = 3;
                    gameManager.character1.GetComponent<CharacterScript>().movementSpeed = 7;
                }
                else
                {
                    movementSpeed = 3;
                    gameManager.character1.GetComponent<CharacterScript>().movementSpeed = 0;
                }
            }
        }
        else
        {
            gameManager.character1.GetComponent<CharacterScript>().movementSpeed = 5;
            gameManager.character2.GetComponent<CharacterScript>().movementSpeed = 5;
        }
    }

}
