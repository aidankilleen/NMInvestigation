using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOrRetreatAIAgent : BaseAIAgent
{
    public Transform safePlace;

    protected override void OnStart()
    {
        
    }

    protected override  void OnUpdate()
    {
        if (currentState == AgentStates.Chase)
        {
            DoChase();
        }
        else if (currentState == AgentStates.Flee)
        {
            agent.SetDestination(safePlace.position);
        }
    }
}
