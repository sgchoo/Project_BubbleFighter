using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrajectoryLine : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    Transform ReleasePosition;
    [Header("Display Controls")]
    [Range(10, 100)]
    public int LinePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float TimeBetweenPoints = 0.1f;
    [SerializeField, Min(3)]
    int lineSegmants = 100;
    [SerializeField, Min(1)]
    float timeOfTheFlight = 5;

    public void ShowLine(Vector3 startPoint, Vector3 startVelocity)
    {
        float timeStep = timeOfTheFlight / lineSegmants;

        Vector3[] lineRendererPoints = CalculateLine(startPoint, startVelocity, timeStep);

        lineRenderer.positionCount = lineSegmants;
        lineRenderer.SetPositions(lineRendererPoints);
    }

    Vector3[] CalculateLine(Vector3 startPoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[lineSegmants];
        lineRendererPoints[0] = startPoint;

        for(int i = 1; i < lineSegmants; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = startVelocity + progressBeforeGravity - gravityOffset;
            lineRendererPoints[i] = newPosition;
        }
        return lineRendererPoints;
    }
}
