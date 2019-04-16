using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "D__OR_", menuName = "KiCo/Decisions/OR")]
public class OR : Decision
{
    public Decision left;
    public Decision right;

    public override bool Decide(Controller controller)
    {
        return left.Decide(controller) || right.Decide(controller);
    }
}
