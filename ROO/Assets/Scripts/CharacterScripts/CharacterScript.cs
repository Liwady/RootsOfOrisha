using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody rb;
    private Vector2 movement;
    private PlayerControls playerControls;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>(); //lamda expression to preform function
        playerControls.Gameplay.SwitchCharacter.performed += ctx => gameManager.SwitchCharacter();
        playerControls.Gameplay.TriggerAbility.performed += ctx => TriggerAbility();
    }
    private void Update()
    {
        Move();
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
        switch (gameManager.currentAbility)
        {
            //ability 1, gravity 
            case 0:
                break;
            //ability 2: size
            case 1:
                if (gameManager.abilityActive)
                {
                    gameManager.character1.transform.localScale = new Vector3(1.8f, 0.8f, 1);
                    gameManager.character2.transform.localScale = new Vector3(0.8f, 1.8f, 1);
                    gameManager.abilityActive = false;
                }
                else
                {
                    if (gameObject == gameManager.character1)
                    {
                        gameManager.character1.transform.localScale = new Vector3(gameManager.character1.transform.localScale.x * 2, gameManager.character1.transform.localScale.y * 2, 1);
                        gameManager.character2.transform.localScale = new Vector3(gameManager.character2.transform.localScale.x / 2, gameManager.character2.transform.localScale.y / 2, 1);
                    }
                    else
                    {
                        gameManager.character2.transform.localScale = new Vector3(gameManager.character2.transform.localScale.x * 2, gameManager.character2.transform.localScale.y * 2, 1);
                        gameManager.character1.transform.localScale = new Vector3(gameManager.character1.transform.localScale.x / 2, gameManager.character1.transform.localScale.y / 2, 1);
                        gameManager.character2.transform.position = new Vector3(gameManager.character2.transform.position.x, gameManager.character2.transform.position.y + 3, gameManager.character2.transform.position.z);
                    }
                    gameManager.abilityActive = true;
                }
                break;
            //ability 3: combine
            case 2:
                break;
        }
    }
    private void Floating()
    {

    }
}
