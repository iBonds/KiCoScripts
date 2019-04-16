using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "A_PutDownItem_", menuName = "KiCo/Actions/PutDownItem")]
public class PutDownItemAction : Action
{
    public string item_tag;

    public override void Act(Controller controller)
    {
        if (controller.picked_up_item != null)
        {
            PutDownItem(controller);
        }
    }
    public override void SetAnimation(Animator animator)
    {
        IdleAnimation.SetState(animator);
    }

    private void PutDownItem(Controller controller)
    {
        controller.picked_up_item.transform.SetParent(null);
        controller.picked_up_item = null;
    }
}
