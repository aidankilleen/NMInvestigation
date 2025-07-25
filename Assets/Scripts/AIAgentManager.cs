using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class AIAgentManager : MonoBehaviour
{

    public List<BaseAIAgent> agents = new List<BaseAIAgent>();
    public CivilianAIAgent civilianPrefab;// = Resources.Load<CivilianAIAgent>("Prefabs/Civilian");
    public FollowOrRetreatAIAgent followOrRetreatPrefab;

    public SplineContainer civilianSpline;
    public GameObject target;

    private float spawnTimer = 0f;
    public float spawnInterval = 5f;

    public List<Transform> safePlaces = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // create a FollowOrRetreat agent
        SpawnFollowOrRetreat();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0f;
            SpawnCivilian();
        }
            
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnCivilian();
        }
    }

    void SpawnCivilian()
    {
        // instantiate a new Civilian
        CivilianAIAgent civilian = Instantiate<CivilianAIAgent>(civilianPrefab,
                                                                civilianSpline.transform.position,
                                                                Quaternion.identity);
        civilian.spline = civilianSpline;
        civilian.currentState = AgentStates.Patrol;

        agents.Add(civilian);
    }

    void SpawnFollowOrRetreat()
    {
        var safePlace = safePlaces[Random.Range(0, safePlaces.Count)];

        FollowOrRetreatAIAgent forai = Instantiate<FollowOrRetreatAIAgent>(followOrRetreatPrefab,
                                                        safePlaces[0].position,
                                                        Quaternion.identity);
        forai.target = target.transform;
        forai.currentState = AgentStates.Chase;
        forai.safePlace = safePlace;
        agents.Add(forai);
    }

    public void ChangeChaseToFlee()
    {
        foreach (var agent in agents)
        {
            if (agent is FollowOrRetreatAIAgent)
            {
                agent.currentState = AgentStates.Flee;
            }
        }
    }
    public void ToggleChaseOrFlee()
    {
        foreach (var agent in agents)
        {
            if (agent is FollowOrRetreatAIAgent)
            {
                agent.currentState = agent.currentState == AgentStates.Chase 
                                    ? AgentStates.Flee : AgentStates.Chase;
            }
        }
    }
}
