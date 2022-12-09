using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y, rb.velocity.z);
    }

}
