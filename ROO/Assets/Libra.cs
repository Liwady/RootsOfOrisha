using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libra : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform[] points;
    

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
    }
}
