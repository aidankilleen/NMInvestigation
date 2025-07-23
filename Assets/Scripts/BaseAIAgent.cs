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

public enum AgentDirctions
{
    Forward, 
    Backward
}

public class BaseAIAgent : MonoBehaviour
{
    public Transform target; // The target to follow or retreat from
    protected NavMeshAgent agent;

    public AgentStates currentState = AgentStates.Idle;
    public AgentDirctions currentDirection = AgentDirctions.Forward;

    public SplineContainer spline; // Reference to the SplineContainer script
    public float waypointSpacing = 1f; // Distance between waypoints
    public float stopThreshold = 0.2f;

    protected List<Vector3> pathPoints = new List<Vector3>();
    protected int currentIndex = 0;

    protected Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        OnStart();
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;
        animator?.SetFloat("Speed", speed);

        // ? check if animator is null
        //if (animator != null)
        //{
        //  /  animator.SetFloat("Speed", speed);
        //}


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

        float closestDistance = float.MaxValue;

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)pointCount; // evenly distributed [0,1)
            Vector3 localPos = spline.Spline.EvaluatePosition(t);
            Vector3 worldPos = spline.transform.TransformPoint(localPos);
            pathPoints.Add(worldPos);

            // find the closest point on the spline to the agent's position
            float dist = Vector3.Distance(worldPos, transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                currentIndex = i; // Set the current index to the closest point
            }
        }
    }
    protected void GoToNextPoint()
    {
        if (pathPoints.Count == 0) return;

        agent.SetDestination(pathPoints[currentIndex]);

        if (currentDirection == AgentDirctions.Forward) {
            currentIndex = (currentIndex + 1) % pathPoints.Count; // loop back to start
        }
        else
        {
            currentIndex = currentIndex - 1;
            if (currentIndex < 0)
            {
                currentIndex = pathPoints.Count - 1; // loop back to end
            }

        }


    }
}
