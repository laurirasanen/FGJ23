using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootCharacter : MonoBehaviour
{
    public float LineSimplifyThreshold = 0.01f;

    private LineRenderer lineRenderer;
    private int verticesSinceOptimize;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        AddVertex(transform.position);
    }

    public void AddVertex(Vector3 position)
    {
        MoveHead(position);
        lineRenderer.positionCount++;
        verticesSinceOptimize++;
        if (verticesSinceOptimize > 10)
        {
            lineRenderer.Simplify(LineSimplifyThreshold);
            verticesSinceOptimize = 0;
        }
    }

    public void MoveHead(Vector3 position)
    {
        if (lineRenderer.positionCount == 0)
        {
            return;
        }
        
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }
}
