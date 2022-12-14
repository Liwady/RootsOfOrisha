using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    private PlayerControls playerControls;
    private GameManager gameManager;
    public GameObject checker, floatCheck, grabbedObject, detectedObject, grabPointS, grabPointL;
    public Rigidbody rb;
    public bool dead, canMove, usedAbility, abilityTriggered, canResize, canWalkOnWater, isHoldingCollectible, isGrounded;
    public float movementSpeed;
    public int size, weight;
    private Vector2 movement;
    public LeverScript inRangeLever;
    public CollectibleScript.FruitEye typeEF;


    void Awake()
    {
        usedAbility = false;
        canWalkOnWater = false;
        isHoldingCollectible = false;
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
        playerControls.Gameplay.Grab.performed += ctx => GrabObject();
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
            gameManager.SetGravity();
        }
        else if (gameManager.otherChar.isGrounded)
        {

            gameManager.abilityActive = true;
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
        if (gameManager.character1.GetComponent<CharacterScript>().usedAbility)
        {
            gameManager.character1.transform.localScale /= 1.8f;
            gameManager.character2.transform.localScale *= 1.8f;
        }
        else
        {
            gameManager.character1.transform.localScale *= 1.8f;
            gameManager.character2.transform.localScale /= 1.8f;
        }
    }
    public void DefaultValuesFloating()
    {
        if (gameManager.currentChar.gameObject == gameManager.character1)
            gameManager.character2.transform.position = new Vector3(gameManager.character1.transform.position.x, gameManager.character2.transform.position.y, gameManager.character2.transform.position.z);
        else
            gameManager.character1.transform.position = new Vector3(gameManager.character2.transform.position.x, gameManager.character1.transform.position.y, gameManager.character1.transform.position.z);
    }
    private void GrabObject()
    {
        if (detectedObject != null)
        {
            //if  holding item -> drop
            //set bool false, unparent object, grabbed object null
            if (isHoldingCollectible)
            {
                isHoldingCollectible = false;
                grabbedObject.transform.parent = null;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.transform.position = grabbedObject.transform.position;
                grabbedObject = null;
                detectedObject = null;
            }
            //if  holding nothing -> grab
            //set bool to true, parent object to player, grabbed object to object 
            else
            {
                isHoldingCollectible = true;
                grabbedObject = detectedObject;
                grabbedObject.transform.parent = gameObject.GetComponentInChildren<Transform>();
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                if (gameObject == gameManager.character1)
                    grabbedObject.transform.position = grabPointS.transform.position;
                else
                    grabbedObject.transform.position = grabPointL.transform.position;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Statue") && !isHoldingCollectible)
            detectedObject = other.gameObject;
    }
}
