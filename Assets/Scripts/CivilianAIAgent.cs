using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianAIAgent : BaseAIAgent
{
    // Start is called before the first frame update
    override protected void OnStart()
    {
        GeneratePathPoints();
    }

    // Update is called once per frame
    override protected void OnUpdate()
    {
        if (currentState == AgentStates.Patrol)
        {
            if (!agent.pathPending && agent.remainingDistance < stopThreshold)
            {
                GoToNextPoint();
            }
        }
    }
}
