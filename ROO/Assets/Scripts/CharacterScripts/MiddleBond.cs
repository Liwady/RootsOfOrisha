using UnityEngine;

public class MiddleBond : MonoBehaviour
{

    private LineRenderer lineRenderer;

    //Array of transforms representing the two points
    [SerializeField]
    private Transform[] points;

    //Maximum distance allowed between the two points
    [SerializeField]
    public float maxDistance;

    //The actual distance between the two points
    public float distance;

    //Reference to the PlayerManager component
    private PlayerManager playerManager;

    //Boolean variable to keep track if the distance is out of range
    public bool outOfRange;

    void Start()
    {
        //Get the reference to the PlayerManager component
        playerManager = GetComponentInParent<PlayerManager>();

        //Get the reference to the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        //Set up the line
        SetUpLine();
    }

    //Method to set up the line
    void SetUpLine()
    {
        //Set the number of positions in the LineRenderer component
        lineRenderer.positionCount = points.Length;
    }

    private void Update()
    {
        //Update the positions of the points in the LineRenderer component
        for (int i = 0; i < points.Length; i++)
            lineRenderer.SetPosition(i, points[i].position);

        //Check if the distance between the two points is out of range
        CheckOutOfRange();
    }

    //Method to check if the distance between the two points is out of range
    private void CheckOutOfRange()
    {
        //Check which character the player is currently controlling
        if (playerManager.currentCharacter == playerManager.character1script)
            //Calculate the distance between the first point and the second point
            distance = Vector3.Distance(points[0].transform.position, points[1].transform.position);
        else
            //Calculate the distance between the second point and the first point
            distance = Vector3.Distance(points[1].transform.position, points[0].transform.position);

        //Check if the distance is greater than the maximum distance allowed
        if (maxDistance <= distance)
            //If yes, set the outOfRange variable to true
            outOfRange = true;
        else
            //If no, set the outOfRange variable to false
            outOfRange = false;
    }
}
