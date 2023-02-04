using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCharacter : Root
{
    public Branch BranchPrefab;
    public float BranchIntervalMin = 0.5f;
    public float BranchIntervalMax = 1.5f;

    private SphereCollider sphereCollider;
    private float nextBranchTime;

    public override void Start()
    {
        base.Start();
        sphereCollider = GetComponent<SphereCollider>();
        lineRenderer.material.SetFloat("_YOffset", 0.1f);
        nextBranchTime = Time.time + BranchIntervalMax;
    }

    public override void AddVertex(Vector3 position, Vector3 direction)
    {
        base.AddVertex(position, direction);
    }

    public override Vector3 MoveHead(Vector3 position, Vector3 direction)
    {
        direction = base.MoveHead(position, direction);

        var colliders = Physics.OverlapSphere(position, 0.15f);
        foreach (var c in colliders)
        {
            var human = c.GetComponent<Human>();
            if (human)
            {
                human.Explode();
                // Add new branch immediately
                nextBranchTime = 0;
            }
        }

        TryAddBranch(position, direction);
        return direction;
    }

    private void TryAddBranch(Vector3 position, Vector3 direction)
    {
        if (Time.time > nextBranchTime)
        {
            var quat = Quaternion.LookRotation(direction, Vector3.up);
            quat *= Quaternion.Euler(0, Random.value * 360.0f, 0);
            Instantiate(BranchPrefab, position, quat);
            nextBranchTime = Time.time + BranchIntervalMin + Random.value * (BranchIntervalMax - BranchIntervalMin);
        }
    }
}
