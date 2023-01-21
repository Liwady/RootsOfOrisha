using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player,switchPoint;        //Public variable to store a reference to the player game object
    [SerializeField]
    private float upBound, downBound, leftBound, rightBound, offsettUpDown, offsettLeftRight;
    public int currentLevel=1;
    private bool switchBounds;

    void Update()
    {
        if(currentLevel==1 && !switchBounds)
            transform.position = player.transform.position + new Vector3(0, 6, -5);
        else
            transform.position = player.transform.position + new Vector3(0, 4, -5);
        transform.position = new Vector3 //keep camera in boundary
        (
            Mathf.Clamp(transform.position.x, leftBound + offsettLeftRight, rightBound - offsettLeftRight),
            Mathf.Clamp(transform.position.y, downBound + offsettUpDown, upBound - offsettUpDown),
            transform.position.z
        );
        if(currentLevel == 1)
        {
            switchBounds = switchPoint.GetComponent<CameraSwitch>().switchBounds;
            Level1();
        }
        
    }
    private void Level1()
    {
        if(switchBounds)
        {
            upBound = 15;
            downBound = 0.2f;
            leftBound = 0.16f;
            rightBound = 78;
            offsettLeftRight = 9;
            offsettUpDown = 6.4f;
        }
        else
        {
            upBound = 29.5f;
            downBound = 0.2f;
            leftBound = -0.4f;
            rightBound = 26;
            offsettLeftRight = 11.3f;
            offsettUpDown = 6.4f;
        }

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
