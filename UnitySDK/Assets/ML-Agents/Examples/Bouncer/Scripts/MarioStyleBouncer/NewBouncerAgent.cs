using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

/// <summary>
/// This goes into the agent. Requires BouncerCoinManager to work.
/// </summary>
[RequireComponent(typeof(BouncerCoinManager))]
public class NewBouncerAgent : Agent
{
    [Header("Bouncer Specific")]
    public GameObject banana;
    public GameObject bodyObject;
    Rigidbody rb;
    Vector3 lookDir;
    public float strength = 10f;
    float jumpCooldown;
    int numberJumps = 20;
    int jumpLeft = 20;
    float distanceWithGoal = 1000000;
    float lastDistanceWithGoal = 100000000;
    Vector3 originalLocation;

    public override void InitializeAgent()
    {
        distanceWithGoal = 0;
        originalLocation = this.transform.localPosition;
        rb = gameObject.GetComponent<Rigidbody>();
        lookDir = Vector3.zero;
    }

    public override void CollectObservations()
    {
        AddVectorObs(gameObject.transform.localPosition);
        AddVectorObs(banana.transform.localPosition);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        for (int i = 0; i < vectorAction.Length; i++)
        {
            vectorAction[i] = Mathf.Clamp(vectorAction[i], -1f, 1f);
        }
        float x = vectorAction[0];
        float y = ScaleAction(vectorAction[1], 0, 1);
        float z = vectorAction[2];
        rb.AddForce(new Vector3(x, y + 1, z) * strength);

        #region Rewards
        //Gives a positive reward if he goes forward, if he goes backwards he gets a negative reward.
        distanceWithGoal = Vector3.Distance(this.transform.localPosition, BouncerCoinManager.Instance.EndGoal);
        if (distanceWithGoal < lastDistanceWithGoal)
        {
            AddReward(0.05f);
        }
        else { AddReward(-0.05f); }
        lastDistanceWithGoal = distanceWithGoal;
        #endregion

        lookDir = new Vector3(x, y, z);
    }

    public override void AgentReset()
    {

        gameObject.transform.localPosition = originalLocation;
        rb.velocity = default(Vector3);
        jumpLeft = numberJumps;
        this.transform.parent.Rotate(new Vector3(0, 1, 0), Random.Range(0, 361));   //Rotate environment
        BouncerCoinManager.Instance.Restart();
    }

    public override void AgentOnDone()
    {

    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), 0.51f) && jumpCooldown <= 0f)
        {
            RequestDecision();
            jumpLeft -= 1;
            jumpCooldown = 0.1f;
            rb.velocity = default(Vector3);
        }

        jumpCooldown -= Time.fixedDeltaTime;

        if (gameObject.transform.position.y < -1)
        {
            AddReward(-1);
            Done();
            return;
        }

        if (jumpLeft == 0)
        {
            Done();
        }

    }

    private void Update()
    {
        if (lookDir.magnitude > float.Epsilon)
        {
            bodyObject.transform.rotation = Quaternion.Lerp(bodyObject.transform.rotation,
                Quaternion.LookRotation(lookDir),
                Time.deltaTime * 10f);
        }
    }
}
