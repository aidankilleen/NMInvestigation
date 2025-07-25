using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChaseToFlee : MonoBehaviour
{

    private AIAgentManager agentManager;

    
    // Start is called before the first frame update
    void Start()
    {
        agentManager = FindObjectOfType<AIAgentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player hits this then change the FollowOrFlee agents 
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player hit the trigger object");

                agentManager.ChangeChaseToFlee();
            }
        }
    }
}
