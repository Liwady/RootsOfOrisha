using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 3, -5);
    }
}
