using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAble : MonoBehaviour
{
    enum Mode
    {
        up,
        down,
        weightdown,
        weightup,
    }

    GameManager gameManager;

    [SerializeField]
    private Vector3 movementVector;
    [SerializeField]
    private Vector3 maxPos;

    private Vector3 originalPos;
   
    private bool triggered;
    private int weightOnMe;

    [SerializeField]
    private Mode mode;

    private void Start()
    {
        originalPos = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Toggle(bool _value)
    {
        triggered = _value;
    }



    private void Update()
    {
        switch (mode)
        {
            case Mode.up:
                if (triggered)
                {
                    if (transform.localPosition.y < maxPos.y)
                    {
                        transform.Translate(movementVector);
                    }
                }
                else if (transform.position.y > originalPos.y)
                {
                    transform.Translate(-movementVector);
                }
                break;
            case Mode.down:
                if (triggered)
                {
                    if (transform.localPosition.y > maxPos.y) //max pos downwards 
                    {
                        transform.Translate(-movementVector);
                    }
                }
                else if (transform.position.y < originalPos.y)
                {
                    transform.Translate(movementVector);
                }
                break;
            case Mode.weightdown:
                if (triggered)
                {
                    maxPos = new Vector3(0, movementVector.y * gameManager.currentChar.GetComponent<CharacterScript>().weight, 1);
                    if (transform.localPosition.y > maxPos.y)
                    {
                        transform.Translate(-movementVector);
                    }
                }
                else if(transform.position.y < originalPos.y)
                {
                    transform.Translate(movementVector);
                }
                break;
            case Mode.weightup:
                if (triggered)
                {
                    maxPos = new Vector3(0, movementVector.y * gameManager.currentChar.GetComponent<CharacterScript>().weight, 1);
                    if (transform.localPosition.y < maxPos.y)
                    {
                        transform.Translate(movementVector);
                    }
                }
                else if (transform.position.y > originalPos.y)
                {
                    transform.Translate(-movementVector);
                }
                break;
        }
    }
}
