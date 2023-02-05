using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    public float TurnSpeed = 180.0f;
    public float SlitherSpeed = 5.0f;
    public float SlitherAngle = 60.0f;
    public Vector3 CameraPositionOffset = new Vector3(0, 10.0f, -3.0f);
    public float StartDelay = 1.0f;
    public float StartLerp = 1.0f;

    private RootCharacter rootCharacter;
    private Vector3 headPosition;
    private Vector3 headDirection;
    private float startTime;
    private float startInterp;

    void Start()
    {
        rootCharacter = GetComponent<RootCharacter>();
        headDirection = transform.forward;
        headPosition = transform.position;
        startTime = Time.time;
        Camera.main.transform.position = headPosition + CameraPositionOffset;
    }

    void Update()
    {
        if (Time.timeScale < float.Epsilon)
        {
            return;
        }

        // wait before moving
        if (Time.time - startTime < StartDelay)
        {
            return;
        }

        // start slowly after delay ends
        startInterp = Mathf.Lerp(0.0f, 1.0f, (Time.time - (startTime + StartDelay)) / StartLerp);
        startInterp = Mathf.Clamp01(startInterp);

        var horizontal = Input.GetAxis("Horizontal");
        Quaternion quat;

        if (Mathf.Abs(horizontal) > Mathf.Epsilon)
        {
            quat = Quaternion.Euler(0, horizontal * TurnSpeed * Time.deltaTime, 0);
        }
        else
        {
            quat = Quaternion.Euler(0, Mathf.Sin(Time.time * SlitherSpeed) * SlitherAngle * Time.deltaTime, 0);
        }

        headDirection = quat * headDirection;
        headPosition += headDirection * rootCharacter.MoveSpeed * Time.deltaTime * startInterp;

        headDirection = rootCharacter.MoveHead(headPosition, headDirection);
        Camera.main.transform.position = headPosition + CameraPositionOffset;
    }

    void FixedUpdate()
    {
        if (Time.timeScale < float.Epsilon)
        {
            return;
        }

        rootCharacter.AddVertex(headPosition, headDirection);
    }
}
