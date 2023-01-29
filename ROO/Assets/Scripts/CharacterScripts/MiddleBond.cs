using UnityEngine;

public class MiddleBond : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private float maxDistance, distance;
    private PlayerManager playerManager;

    public bool outOfRange;
    void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        lineRenderer = GetComponent<LineRenderer>();
        SetUpLine();
    }
    void SetUpLine()
    {
        lineRenderer.positionCount = points.Length;
    }
    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
        if (playerManager.usedMove)
            CheckOutOfRange();
    }
    private void CheckOutOfRange()
    {
        if (playerManager.currentCharacter == playerManager.character1script)
            distance = Vector3.Distance(points[0].transform.position, points[1].transform.position);
        else
            distance = Vector3.Distance(points[1].transform.position, points[0].transform.position);
        Debug.Log(distance);
        if (maxDistance <= distance)
            outOfRange = true;
        else
            outOfRange = false;
    }
}
