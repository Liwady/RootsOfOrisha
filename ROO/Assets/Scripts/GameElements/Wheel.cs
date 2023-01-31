using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    private GameManager gameManager;

    [SerializeField]
    private int wheelNum;

    private bool rotating;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void RotateWheelForward(bool _forward) 
    {
        if (_forward)
        {
            gameManager.PlaySound("wheel", wheelNum);
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
            rotating = true;
        }
    }


}
