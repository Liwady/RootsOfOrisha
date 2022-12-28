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
    public bool dead, canGrab, canMove, usedAbility, abilityTriggered;
    public int size;
    public int weight;
    WaterState statusW;
    public lever inRangeLever;
    public bool isHoldingCollectible = false; //for other char to collect
    public CollectibleScript.FruitEye typeEF;
    private enum WaterState
    {
        CanWalk,
        Sink,
        Die
    }

    void Awake()
    {
        usedAbility = false;
        statusW = WaterState.CanWalk;
        canResize = true;
        canMove = true;
        rb = GetComponentInChildren<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>(); //lamda expression to preform function
        playerControls.Gameplay.SwitchCharacter.performed += ctx => gameManager.SwitchCharacter();
        playerControls.Gameplay.SwitchAbility.performed += ctx => gameManager.SwitchAbility();
        playerControls.Gameplay.TriggerAbility.performed += ctx => gameManager.TriggerAbility();
        playerControls.Gameplay.ToggleButton.performed += ctx => gameManager.ToggleLever();
        playerControls.Gameplay.Respawn.performed += ctx => gameManager.RespawnCharacters();
    }
    private void Update()
    {
        if (canMove)
            Move();
        if (gameManager.currentAbility == 1 && gameManager.abilityActive && usedAbility)
            MoveTowardsPlace();
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
    public void Floating()
    {
        usedAbility = true;
        if (gameManager.abilityActive)
        {
            gameManager.abilityActive = false;
            usedAbility = false;
            DefaultValuesFloating();
            gameManager.SetGravity();
        }
        else
        {
            SetUsedAbility();
            MoveTowardsPlace();
        }
    }
    public void Sizing()
    {

        if (canResize || usedAbility)//todo checker for mini and make size table 
        {
            if (gameManager.abilityActive)
            {
                DefaultValuesSize();
                gameManager.abilityActive = false;
                usedAbility = false;
            }
            else
            {
                gameManager.abilityActive = true;
                SetUsedAbility();
                gameManager.SetSize();
                gameManager.SetGravity();
            }

        }
    }
    private void SetUsedAbility()
    {

        usedAbility = true;
        if (gameObject == gameManager.character1)
            gameManager.character2.GetComponent<CharacterScript>().usedAbility = false;
        else
            gameManager.character1.GetComponent<CharacterScript>().usedAbility = false;

    }
    private void MoveTowardsPlace()
    {
        if (gameManager.currentChar.gameObject == gameManager.character1)
            gameManager.character2.transform.position = Vector3.MoveTowards(gameManager.character2.transform.position, gameManager.character1.GetComponent<CharacterScript>().floatCheck.transform.position, 5 * Time.deltaTime);
        else
            gameManager.character1.transform.position = Vector3.MoveTowards(gameManager.character1.transform.position, gameManager.character2.GetComponent<CharacterScript>().floatCheck.transform.position, 5 * Time.deltaTime);
    }
    public void DefaultValuesSize()
    {
        gameManager.character1.transform.localScale = new Vector3(0.9f, 0.4f, 1);
        gameManager.character2.transform.localScale = new Vector3(0.8f, 1.6f, 1);
    }
    public void DefaultValuesFloating()
    {
        if (gameManager.currentChar.gameObject == gameManager.character1)
            gameManager.character2.transform.position = new Vector3(gameManager.character1.transform.position.x, gameManager.character2.transform.position.y, 1);
        else
            gameManager.character1.transform.position = new Vector3(gameManager.character2.transform.position.x, gameManager.character1.transform.position.y, 1);
    }
}
