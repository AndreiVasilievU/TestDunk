using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] points;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector2 origin, Vector2 speed)
    {
        points = new Vector3[10];
        lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics2D.gravity * time * time / 2f;
        }

        lineRenderer.SetPositions(points);
    }
    
    public void ClearPointsTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}
