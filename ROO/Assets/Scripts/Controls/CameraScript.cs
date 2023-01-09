using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Public variable to store a reference to the player game object
    public GameObject player;
    [SerializeField]
    private float upBound = 10, rightBound = 10, leftBound = -10, downBound = -10;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 3, -5);
        transform.position = new Vector3 //keep camera in boundary
        (
            Mathf.Clamp(transform.position.x, leftBound , rightBound),
            Mathf.Clamp(transform.position.y, downBound , upBound),
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
