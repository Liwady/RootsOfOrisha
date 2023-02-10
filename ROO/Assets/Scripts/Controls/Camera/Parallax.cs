using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform cTransform; // reference to the main camera's transform
    private Vector3 lastCameraPosition; // to store the position of the camera in the previous frame
    public float parallaxEffect = 0.5f; // the strength of the parallax effect, the default value is 0.5

    private void Start()
    {
        // get the reference to the main camera's transform
        cTransform = Camera.main.transform;
        // store the position of the camera in the previous frame
        lastCameraPosition = cTransform.position;
    }

    private void LateUpdate()
    {
        // calculate the change in movement of the camera
        float delMovement = cTransform.position.x - lastCameraPosition.x;
        // store the position of the camera in the current frame for the next iteration
        lastCameraPosition = cTransform.position;
        // update the position of the current object using the parallax effect formula
        transform.position -= new Vector3(delMovement * parallaxEffect, 0);
    }
}
