using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootCharacter : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float LineSimplifyThreshold = 0.01f;
    public float LengthToEndColor = 100.0f;

    private LineRenderer lineRenderer;
    private SphereCollider sphereCollider;
    private int verticesSinceOptimize;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        lineRenderer.positionCount = 0;
        AddVertex(transform.position);
    }

    public void AddVertex(Vector3 position)
    {
        MoveHead(position);
        lineRenderer.positionCount++;
        verticesSinceOptimize++;
        if (verticesSinceOptimize > 100)
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

        // adjust color gradient so that the tail doesn't
        // take longer to change color the longer the root.
        var currentLength = Time.time * MoveSpeed;
        var frac = 1.0f - LengthToEndColor / currentLength;
        if (frac > 0)
        {
            var temp = lineRenderer.colorGradient;
            var tempKeys = temp.colorKeys;
            tempKeys[0].time = frac;
            temp.colorKeys = tempKeys;
            lineRenderer.colorGradient = temp;
        }

        var colliders = Physics.OverlapSphere(position, 0.15f);
        foreach (var c in colliders)
        {
            var human = c.GetComponent<Human>();
            human?.Explode();
        }
    }
}
