﻿using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "A_RunFrom_", menuName = "KiCo/Actions/RunFrom")]
public class RunFromAction : Action
{
    public string tag_target;
    public float speed = 1f;

    public override void Act(Controller controller)
    {
        RunFrom(controller);
    }

    public override void SetAnimation(Animator animator)
    {
        WalkAnimation.SetState(animator);
    }

    private void RunFrom(Controller controller)
    {
        if (controller.range.InRange(tag_target))
        {
            if (controller.agent.isStopped)
                controller.agent.isStopped = false;

            Vector3 direction = controller.transform.position - controller.range.Position(tag_target);
            controller.agent.speed = speed;
            controller.agent.SetDestination(controller.transform.position + direction);
        }
    }
}
