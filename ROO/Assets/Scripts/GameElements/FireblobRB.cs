using UnityEngine;

public class FireblobRB : MonoBehaviour
{
    private Rigidbody myRB;
    private PlayerManager playerManager;
    private float startposy;
    [SerializeField]
    public int force;
    private void Awake()
    {
        myRB = this.GetComponent<Rigidbody>();
        playerManager = FindObjectOfType<PlayerManager>();
        startposy = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < startposy)
            myRB.velocity = new Vector3(0, force, 0);
    }

    private void OnTriggerEnter(Collider other) //checks if the player collided with the fireblob
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
            playerManager.RespawnCharacters();
    }

}
