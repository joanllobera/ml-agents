using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Agent agent = collision.gameObject.GetComponent<Agent>();
        if (agent != null)
        {
            agent.AddReward(1f);
            agent.AgentReset();
        }

    }
}
