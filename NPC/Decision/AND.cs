using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "D__AND_", menuName = "KiCo/Decisions/AND")]
public class AND : Decision
{
    public Decision left;
    public Decision right;

    public override bool Decide(Controller controller)
    {
        return left.Decide(controller) && right.Decide(controller);
    }
}
