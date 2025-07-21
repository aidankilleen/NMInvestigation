using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;


public enum AgentStates {
    Idle, 
    Patrol, 
    Chase, 
    Flee, 
    Dead
}

public class BaseAIAgent : MonoBehaviour
{
    public Transform target; // The target to follow or retreat from
    protected NavMeshAgent agent;

    public AgentStates currentState = AgentStates.Idle;

    public SplineContainer spline; // Reference to the SplineContainer script
    public float waypointSpacing = 1f; // Distance between waypoints
    public float stopThreshold = 0.2f;

    protected List<Vector3> pathPoints = new List<Vector3>();
    protected int currentIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnUpdate()
    {
        if (currentState == AgentStates.Chase)
        {
            DoChase();
        }
    }

    protected void DoChase()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
    protected void GeneratePathPoints()
    {
        pathPoints.Clear();

        // if spline is not set - do nothing
        if (spline == null || spline.Spline == null)
        {
            return;
        }

        float splineLength = spline.Spline.GetLength();
        int pointCount = Mathf.CeilToInt(splineLength / waypointSpacing);

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)pointCount; // evenly distributed [0,1)
            Vector3 localPos = spline.Spline.EvaluatePosition(t);
            Vector3 worldPos = spline.transform.TransformPoint(localPos);
            pathPoints.Add(worldPos);
            Debug.Log(worldPos);

        }
    }
    protected void GoToNextPoint()
    {
        if (pathPoints.Count == 0) return;

        agent.SetDestination(pathPoints[currentIndex]);
        currentIndex = (currentIndex + 1) % pathPoints.Count; // loop back to start
    }
}
