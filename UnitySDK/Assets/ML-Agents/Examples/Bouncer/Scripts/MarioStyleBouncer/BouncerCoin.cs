using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

/// <summary>
/// Goes into the coin gameobject. Requires a collider in trigger mode and a rigidbody for the trigger to work.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BouncerCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Agent agent = collision.gameObject.GetComponent<Agent>();
        if (agent != null)
        {
            agent.AddReward(1f);
            BouncerCoinManager.Instance.CoinCollected(this.transform);
        }

    }
}