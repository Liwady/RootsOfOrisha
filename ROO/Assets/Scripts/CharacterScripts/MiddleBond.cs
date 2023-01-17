using UnityEngine;

public class MiddleBond : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private float maxDistance;

    public bool outOfRange;
    void Start()
    {
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
        if (maxDistance <= Vector3.Distance(points[0].transform.position, points[1].transform.position))
            outOfRange = true;
        else
            outOfRange = false;
    }
}
