using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotator : MonoBehaviour
{
    [SerializeField] private TrajectoryRenderer trajectoryRenderer;
    private enum Side
    {
        Left = -1,
        Right = 1
    }

    private Vector3 startMouseDirection;
    private Vector3 currentMouseDirection;

    private Vector3 pointMousePosition;
    private Vector3 newPointMousePosition;

    private Transform thisTransform;
    private Camera mainCamera;
    
    public Vector2 CurrentMouseDirection
    {
        get { return currentMouseDirection; }
        set { currentMouseDirection = value; }
    }

    public Vector2 StartMouseDirection
    {
        get { return startMouseDirection; }
        set { startMouseDirection = value; }
    }

    public bool isActive;

    private void Start()
    {
        mainCamera = Camera.main;
        thisTransform = transform;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pointMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                newPointMousePosition = pointMousePosition + Vector3.right;
                startMouseDirection = newPointMousePosition - pointMousePosition;
            }
            else if (Input.GetMouseButton(0) && isActive == true)
            {
                currentMouseDirection = newPointMousePosition - mainCamera.ScreenToWorldPoint(Input.mousePosition);

                if (startMouseDirection != currentMouseDirection)
                {
                    float angleZ = GetValueZ();
                    thisTransform.rotation = Quaternion.Euler(0f, 0f, angleZ - 90);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                trajectoryRenderer.ClearPointsTrajectory();
            }
        }
    }

    private Side GetSide()
    {
        Side side = Side.Right;

        if (currentMouseDirection.y <= startMouseDirection.y)
        {
            side = Side.Left;
        }

        return side;
    }

    private float GetValueZ()
    {
        float scalarComposition = startMouseDirection.x * currentMouseDirection.x + startMouseDirection.y * currentMouseDirection.y;
        float modulesComposition = startMouseDirection.magnitude * currentMouseDirection.magnitude;
        float division = scalarComposition / modulesComposition;
        float angle = Mathf.Acos(division) * Mathf.Rad2Deg * (int)GetSide();
        return angle;
    }
}
