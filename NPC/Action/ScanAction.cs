using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "A_Scan_", menuName = "KiCo/Actions/Scan")]
public class ScanAction : Action
{
    public override void Act(Controller controller)
    {
        if (!controller.agent.isStopped)
            controller.agent.isStopped = true;
        controller.transform.Rotate(0, 30 * Time.deltaTime, 0);
    }

    public override void SetAnimation(Animator animator)
    {
        IdleAnimation.SetState(animator);
    }
}
