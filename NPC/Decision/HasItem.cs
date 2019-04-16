using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "D_HasItem_", menuName = "KiCo/Decisions/HasItem")]
public class HasItem : Decision
{
    public string tag;

    public override bool Decide(Controller controller)
    {
        if(controller.picked_up_item != null)
        {
            return controller.picked_up_item.CompareTag(tag);
        }

        return false;
    }
}
