using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform cTransform;
    private Vector3 lastCameraPosition;
    public float parallaxEffect = 0.5f;
    private void Start()
    {
        cTransform= Camera.main.transform;
        lastCameraPosition=cTransform.position;
    }
    private void LateUpdate()
    {
        float delMovement = cTransform.position.x - lastCameraPosition.x;
        lastCameraPosition = cTransform.position;
        transform.position -= new Vector3(delMovement*parallaxEffect,0);
        
        
    }
}
