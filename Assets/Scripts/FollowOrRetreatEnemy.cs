using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowOrRetreatEnemy : MonoBehaviour
{

    public Transform target;
    public bool following = true;
    public List<GameObject> safeAreas = new List<GameObject>();

    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            following = !following;
            if (!following)
            {
                var safeArea = GetFurthestSafeArea();
                agent.SetDestination(safeArea.transform.position);
            }
        }

        if (following)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    private GameObject GetFurthestSafeArea()
    {
        float maxDistance = 0;
        GameObject safeArea = null;

        foreach (var area in safeAreas)
        {
            float distance = Vector3.Distance(target.position, area.transform.position);

            if (distance > maxDistance)
            {
                maxDistance = distance;
                safeArea = area;
            }
        }
        return safeArea;
    }
}
