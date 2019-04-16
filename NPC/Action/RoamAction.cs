﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "A_Roam_", menuName = "KiCo/Actions/Roam")]
public class RoamAction : Action
{

    public Vector3[] waypoints;
    public int waypoint = 0;
    public float speed = 1f;

    private void Awake()
    {
        waypoint = 0;
    }

    private void OnEnable()
    {
        waypoint = 0;
    }
    public override void SetAnimation(Animator animator)
    {
        WalkAnimation.SetState(animator);
    }

    public override void Act(Controller controller)
    {
        Roam(controller);
    }
    
    private void Roam(Controller controller)
    {
        if (controller.agent.isStopped)
        {
            controller.agent.isStopped = false;
        }

        if (!controller.agent.pathPending && controller.agent.remainingDistance < 0.5f)
        {
            controller.agent.destination = waypoints[waypoint];
            controller.agent.speed = speed;
            waypoint = (waypoint == waypoints.Length - 1) ? 0 : waypoint + 1;
        }
    }
}
