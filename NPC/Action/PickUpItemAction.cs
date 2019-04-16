using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "A_PickUpItem_", menuName = "KiCo/Actions/PickUpItem")]
public class PickUpItemAction : Action
{
    public string item_tag;

    public override void Act(Controller controller)
    {
        if (controller.picked_up_item == null)
        {
            PickUpItem(controller);
        }
    }

    public override void SetAnimation(Animator animator)
    {
        IdleAnimation.SetState(animator);
    }

    private void PickUpItem(Controller controller)
    {
        if (controller.range.InRange(item_tag))
        {
            GameObject gameObject = controller.range.Item(item_tag);
            gameObject.transform.SetParent(controller.item_loc.transform);
            gameObject.transform.localPosition = Vector3.zero;
            controller.picked_up_item = gameObject;
        }
    }
}
