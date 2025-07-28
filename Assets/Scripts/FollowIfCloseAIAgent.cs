using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FollowIfCloseAIAgent : BaseAIAgent
{
    public float followThreshold = 5f;

    override protected void OnStart()
    {
        
    }

    override protected void OnUpdate()
    {
        if (target == null)
        {
            return; // No target to follow
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        Debug.Log($"{distanceToTarget}");

        if (distanceToTarget < followThreshold)
        {
            currentState = AgentStates.Chase; // Switch to Chase state if within threshold
        }
        else
        {
            currentState = AgentStates.Idle; // Switch to Idle state if outside threshold
        }

        if (currentState == AgentStates.Chase)
        {
            DoChase();
        }

    }
}
