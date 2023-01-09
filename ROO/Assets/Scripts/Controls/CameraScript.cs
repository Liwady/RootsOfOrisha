using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object
    [SerializeField]
    private float upBound, downBound, leftBound, rightBound, offsettUpDown, offsettLeftRight;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 3, -5);
        transform.position = new Vector3 //keep camera in boundary
        (
            Mathf.Clamp(transform.position.x, leftBound + offsettLeftRight, rightBound - offsettLeftRight),
            Mathf.Clamp(transform.position.y, downBound + offsettUpDown, upBound - offsettUpDown),
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
