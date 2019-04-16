using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "D_NOT_", menuName = "KiCo/Decisions/NOT")]
public class Negation : Decision
{
    public Decision decision;

    public override bool Decide(Controller controller)
    {
        return !decision.Decide(controller);
    }
}
