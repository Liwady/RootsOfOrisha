using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float movementSpeed;
    public float jumpStrength;
    private Rigidbody rb;
    private bool grounded;
    public Vector3 jump;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded)
            Jump();
        Move();
    }
    void OnCollisionStay()
    {
        grounded = true;
    }
    private void Move()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y, rb.velocity.z);
    }
    private void Jump()
    {
        rb.AddForce(jump * jumpStrength, ForceMode.Impulse);
        grounded = false;
    }

}
