using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "D_NearBy_", menuName = "KiCo/Decisions/NearBy")]
public class TargetNearBy : Decision
{
    public string target;
    public override bool Decide(Controller controller)
    {
        return controller.range.InRange(target);
    }
}
