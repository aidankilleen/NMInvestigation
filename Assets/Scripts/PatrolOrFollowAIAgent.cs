using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.Rendering.HableCurve;

public class PatrolOrFollowAIAgent : BaseAIAgent
{


    public float followThreshold = 5f; // if distance less than this value, follow the target



    protected override void OnStart()
    {
        Debug.Log("PatrolOrFollowAIAgent started");
        GeneratePathPoints();
    }

    protected override void OnUpdate()
    {
        if (currentState == AgentStates.Patrol)
        {
            if (!agent.pathPending && agent.remainingDistance < stopThreshold)
            {
                GoToNextPoint();
            }
            

        }
        else if (currentState == AgentStates.Chase)
        {
            DoChase();
        }
    }

}
