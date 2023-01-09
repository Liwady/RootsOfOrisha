using UnityEngine;

public class FireblobRB : MonoBehaviour
{
    private Rigidbody myRB;
    private GameManager gameManager;
    private float startposy;
    [SerializeField]
    public int force;
    private void Awake()
    {
        myRB = this.GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        startposy = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < startposy)
            myRB.velocity = new Vector3(0, force, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("1") || other.CompareTag("2"))
            gameManager.RespawnCharacters();
    }

}
