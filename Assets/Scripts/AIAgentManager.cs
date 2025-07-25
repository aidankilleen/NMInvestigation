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

        FollowOrRetreatAIAgent forai = Instantiate<FollowOrRetreatAIAgent>(followOrRetreatPrefab,
                                                                safePlaces[0].position,
                                                                Quaternion.identity);
        forai.target = target.transform;
        forai.currentState = AgentStates.Chase;

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
}
