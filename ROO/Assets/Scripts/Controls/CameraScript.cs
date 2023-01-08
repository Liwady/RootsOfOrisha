using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object
    [SerializeField]
    private float upBound = 10;
    [SerializeField]
    private float downBound = -10;
    [SerializeField]
    private float leftBound = -10;
    [SerializeField]
    private float rightBound = 10;



    [SerializeField]
    private float upDownSize = 5;
    [SerializeField]
    private float leftRightSize = 14.8f;

    /*[SerializeField]
    private float offsettUpDown;
    [SerializeField]
    private float offsettLeftRight; */

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 3, -5);

        transform.position = new Vector3 //keep camera in boundary
        (
            Mathf.Clamp(transform.position.x, leftBound+ leftRightSize, rightBound- leftRightSize),
            Mathf.Clamp(transform.position.y, downBound+ upDownSize, upBound- upDownSize),
            transform.position.z
        );
    }

    private void OnDrawGizmos()
    {
        //draw a box around camera boundry
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftBound, upBound), new Vector3(rightBound, upBound));
        Gizmos.DrawLine(new Vector2(rightBound, upBound), new Vector3(rightBound, downBound));
        Gizmos.DrawLine(new Vector2(rightBound, downBound), new Vector3(leftBound, downBound));
        Gizmos.DrawLine(new Vector2(leftBound, downBound), new Vector3(leftBound, upBound));
    }
}
