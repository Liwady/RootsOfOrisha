using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAble : MonoBehaviour
{
    enum MoveDir
    {
        up,
        down,
    }

    [SerializeField]
    private Vector3 movementVector;
    [SerializeField]
    private Vector3 maxPos;

    private Vector3 originalPos;
   
    private bool triggered;

    [SerializeField]
    private MoveDir MoveDirection;

    private void Start()
    {
        originalPos = transform.position;
    }
    public void Toggle(bool _value)
    {
        triggered = _value;
    }


    private void Update()
    {
        switch (MoveDirection)
        {
            case MoveDir.up:
                if (triggered)
                {
                    if (transform.position.y < maxPos.y)
                    {
                        transform.Translate(movementVector);
                    }
                }
                else if (transform.position.y > originalPos.y)
                {
                    transform.Translate(-movementVector);
                }
                break;
            case MoveDir.down:
                if (triggered)
                {
                    if (transform.position.y > maxPos.y) //max pos downwards 
                    {
                        transform.Translate(movementVector);
                    }
                }
                else if (transform.position.y < originalPos.y)
                {
                    transform.Translate(-movementVector);
                }
                break;
        }

        
    }
}
