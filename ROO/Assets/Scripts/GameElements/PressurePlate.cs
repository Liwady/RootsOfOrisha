using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private TriggerAble[] triggeredObjects;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < triggeredObjects.Length; i++)
        {
            triggeredObjects[i].Trigger();
        }
    }
}
