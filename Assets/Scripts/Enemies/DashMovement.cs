using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    [SerializeField]
    private Transform alvo;

    [SerializeField]
    private float moveSpeed = 2.0f;

    [SerializeField]
    [Range(0,1)]
    private float interpolation = 0.1f;

    [SerializeField]
    private float invokeTime = 1.0f;

    [SerializeField]
    private float repeatingInvokeTime = 1.0f;

    private Vector2 targetPosition;

    private bool isMoving = true;

    void Start()
    {
        InvokeRepeating("SetTargetPosition", invokeTime, repeatingInvokeTime);
    }

    void SetTargetPosition()
    {
        targetPosition = alvo.position;
        isMoving = true;
    }

    void Update()
    {
        if(isMoving)
        {
            Movement();
        }
    }

    void Movement()
    {
        transform.position = Vector2.Lerp(transform.position, targetPosition, interpolation * Time.deltaTime * moveSpeed);
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
