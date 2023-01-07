using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireblobRB : MonoBehaviour
{
    private Rigidbody myRB;
    private GameManager gameManager;
    [SerializeField]
    private float thrust;

    private float startposy;

    void Start()
    {
        myRB = this.GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        startposy = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < startposy)
        {
            myRB.velocity = new Vector3(0, 10, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            gameManager.RespawnCharacters();
        }
    }

}
