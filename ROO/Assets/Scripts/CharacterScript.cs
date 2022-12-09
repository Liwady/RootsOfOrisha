using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody rb;
    private Vector2 movement;
    private PlayerControls playerControls;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>(); //lamda expression to preform function
        playerControls.Gameplay.Move.canceled += ctx => rb.velocity = Vector3.zero;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector3(movement.x * movementSpeed, rb.velocity.y, rb.velocity.z);
    }
    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }
}
