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
    public bool CheckCollisions = true;
    public float CollisionLookahead = 0.1f;

    protected LineRenderer lineRenderer;
    private uint verticesSinceOptimize;
    private float startTime;
    private Vector3 lastHeadPosition;

    public virtual void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.SetFloat("_ColorLength", LengthToEndColor);
        lineRenderer.positionCount = 0;
        startTime = Time.time;
        lastHeadPosition = transform.position;
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

    public virtual Vector3 MoveHead(Vector3 position, Vector3 direction)
    {
        if (lineRenderer.positionCount == 0)
        {
            return direction;
        }

        if (CheckCollisions)
        {
            var move = position - lastHeadPosition;
            if (Physics.Raycast(lastHeadPosition, move, out var hit, move.magnitude + CollisionLookahead, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                var flat = Vector3.ProjectOnPlane(direction, hit.normal);
                // add normal so we can't clip into walls by running into
                // them repeatedly
                direction = (flat + hit.normal * 0.5f).normalized;
            }
        }

        lastHeadPosition = position;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);

        lineRenderer.material.SetFloat("_Length", GetLifetime() * MoveSpeed);

        return direction;
    }

    protected float GetLifetime()
    {
        return Time.time - startTime;
    }
}
