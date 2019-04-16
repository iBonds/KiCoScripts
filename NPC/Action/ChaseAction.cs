using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "A_Chase_", menuName = "KiCo/Actions/Chase")]
public class ChaseAction : Action
{
    public string chase_target;
    public bool needToSee = true;
    public float distance_to_target = 1f;
    public float speed = 1f;
    public Vector3 position_of_target;

    public override void Act(Controller controller)
    {
        UnityEngine.AI.NavMeshAgent agent = controller.agent;
        Eyes eyes = controller.eyes;
        Range range = controller.range;

        if (needToSee && eyes.sees(chase_target))
        {
            position_of_target = eyes.position(chase_target);
            //controller.transform.LookAt(target_pos);
            
            
                if (agent.isStopped)
                    agent.isStopped = false;
                controller.agent.speed = speed;
                agent.stoppingDistance = distance_to_target;
                agent.SetDestination(position_of_target);
            
            
        } else if (range.InRange(chase_target))
        {
            position_of_target = range.Position(chase_target);
            if (agent.isStopped)
                    agent.isStopped = false;
                controller.agent.speed = speed;
                agent.stoppingDistance = distance_to_target;
                agent.SetDestination(position_of_target);
            
        }
    }

    public override void SetAnimation(Animator animator)
    {
        WalkAnimation.SetState(animator);
    }
}
