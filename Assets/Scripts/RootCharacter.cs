using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootCharacter : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float TurnSpeed = 180.0f;
    public float LineSimplifyThreshold = 0.01f;

    private LineRenderer lineRenderer;
    private float lastVertexTime;
    private Vector3 headPosition;
    private Vector3 headDirection;
    private int verticesSinceSimplify;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        headDirection = transform.forward;
        lineRenderer.positionCount = 0;
        AddVertex();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontal) > Mathf.Epsilon)
        {
            var quat = Quaternion.Euler(0, horizontal * TurnSpeed * Time.deltaTime, 0);
            headDirection = quat * headDirection;
        }

        headPosition += headDirection * MoveSpeed * Time.deltaTime;

        AddVertex();

        if (verticesSinceSimplify > 10)
        {
            var prevCount = lineRenderer.positionCount;
            lineRenderer.Simplify(LineSimplifyThreshold);
            // Debug.Log($"RootCharacter.AddVertex: simplify {prevCount} -> {lineRenderer.positionCount}");
            verticesSinceSimplify = 0;
        }
    }

    private void AddVertex()
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, headPosition);
        lastVertexTime = Time.time;
        verticesSinceSimplify++;
    }
}
