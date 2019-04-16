using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "A_Move_", menuName = "KiCo/Actions/Move")]
public class MoveAction : Action
{
    public Vector3 location;
    public float speed = 1;
    public override void Act(Controller controller)
    {
        Move(controller);
    }

    public override void SetAnimation(Animator animator)
    {
        WalkAnimation.SetState(animator);
    }

    private void Move(Controller controller)
    {
        if (controller.agent.isStopped)
            controller.agent.isStopped = false;
        controller.agent.speed = speed;
        controller.agent.SetDestination(location);
    }
}
