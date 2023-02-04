using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : Root
{
    public float SlitherSpeed = 5.0f;
    public float SlitherAngle = 60.0f;
    public float MinLength = 10.0f;
    public float MaxLength = 30.0f;

    private Vector3 headPosition;
    private Vector3 headDirection;
    private bool finishedGrowing;
    private float targetLength;

    public override void Start()
    {
        base.Start();
        headDirection = transform.forward;
        headPosition = transform.position;
        lineRenderer.material.SetFloat("_YOffset", 0.1f);
        targetLength = MinLength + Random.value * (MaxLength - MinLength);
    }

    public void Update()
    {
        if (finishedGrowing)
        {
            return;
        }

        // Grow
        var quat = Quaternion.Euler(0, Mathf.Sin(Time.time * SlitherSpeed) * SlitherAngle * Time.deltaTime, 0);
        headDirection = quat * headDirection;
        headPosition += headDirection * MoveSpeed * Time.deltaTime;
        MoveHead(headPosition, headDirection);

        // Max length
        if (GetLifetime() * MoveSpeed > targetLength)
        {
            finishedGrowing = true;
            lineRenderer.Simplify(LineSimplifyThreshold);
        }
    }

    public void FixedUpdate()
    {
        if (finishedGrowing)
        {
            return;
        }

        AddVertex(headPosition, headDirection);
    }
}
