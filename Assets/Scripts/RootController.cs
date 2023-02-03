using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float TurnSpeed = 180.0f;
    public float SlitherSpeed = 5.0f;
    public float SlitherAngle = 60.0f;

    private RootCharacter rootCharacter;
    private Vector3 headPosition;
    private Vector3 headDirection;

    void Start()
    {
        rootCharacter = GetComponent<RootCharacter>();
        headDirection = transform.forward;
        headPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

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
        headPosition += headDirection * MoveSpeed * Time.deltaTime;

        rootCharacter.MoveHead(headPosition);
        Camera.main.transform.position = headPosition + new Vector3(0, 10.0f, 0);
    }

    void FixedUpdate()
    {
        rootCharacter.AddVertex(headPosition);
    }
}
