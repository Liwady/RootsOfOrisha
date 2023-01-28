using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    public void RotateWheelForward(bool _forward) 
    {
        if (!_forward)
            transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
        else
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));

    }
}
