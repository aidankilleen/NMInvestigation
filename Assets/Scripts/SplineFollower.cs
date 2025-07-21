using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;
using Vector3 = UnityEngine.Vector3;

public class SplineFollower : MonoBehaviour
{
    public SplineContainer spline; // Reference to the SplineContainer script
    public float waypointSpacing = 1f; // Distance between waypoints
    public float stopThreshold = 0.2f;

    public float followThreshold = 5f; // if distance less than this value, follow the target

    public Transform target;

    private NavMeshAgent agent;
    private List<Vector3> pathPoints = new List<Vector3>();
    private int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GeneratePathPoints();
        GoToNextPoint();

    }

    // Update is called once per frame
    void Update()
    {
        // calculate distance between enemy and target
        var distance = Vector3.Distance(transform.position, target.position);
        
        if (distance < followThreshold)
        {
            Debug.Log("following...");
            agent.SetDestination(target.position);
            return; // exit early, we are following the target
        }
        else
        {
            // else we'll follow the spline path
            if (!agent.pathPending && agent.remainingDistance < stopThreshold)
            {
                GoToNextPoint();
            }
        }


    }
    void GeneratePathPoints()
    {
        pathPoints.Clear();

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
    void GoToNextPoint()
    {
        if (pathPoints.Count == 0) return;

        agent.SetDestination(pathPoints[currentIndex]);
        currentIndex = (currentIndex + 1) % pathPoints.Count; // loop back to start
    }

}
