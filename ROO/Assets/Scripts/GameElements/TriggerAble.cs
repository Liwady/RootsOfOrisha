using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAble : MonoBehaviour
{
    [SerializeField]
    private Vector3 movementVector;

    private bool triggered;

    public void Trigger()
    {
        triggered = true;
    }
}
