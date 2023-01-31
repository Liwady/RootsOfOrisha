using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSound : MonoBehaviour
{
    private GameManager gameManager;
    private Vector3 orignalPos;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        orignalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != orignalPos)
        {
            gameManager.PlaySound("gate");
            orignalPos = transform.position;
        }
    }
}