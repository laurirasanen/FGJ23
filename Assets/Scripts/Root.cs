using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Root : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float LengthToEndColor = 100.0f;
    public float LineSimplifyThreshold = 0.01f;
    public uint SimplifyInterval = 100;

    protected LineRenderer lineRenderer;
    private uint verticesSinceOptimize;
    private float startTime;

    public virtual void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.SetFloat("_ColorLength", LengthToEndColor);
        lineRenderer.positionCount = 0;
        startTime = Time.time;
        AddVertex(transform.position, transform.forward);
    }

    public virtual void AddVertex(Vector3 position, Vector3 direction)
    {
        MoveHead(position, direction);
        lineRenderer.positionCount++;
        verticesSinceOptimize++;
        if (verticesSinceOptimize > SimplifyInterval)
        {
            lineRenderer.Simplify(LineSimplifyThreshold);
            verticesSinceOptimize = 0;
        }
    }

    public virtual void MoveHead(Vector3 position, Vector3 direction)
    {
        if (lineRenderer.positionCount == 0)
        {
            return;
        }
        
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);

        lineRenderer.material.SetFloat("_Length", GetLifetime() * MoveSpeed);
    }

    protected float GetLifetime()
    {
        return Time.time - startTime;
    }
}
