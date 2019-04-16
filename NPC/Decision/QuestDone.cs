using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "D_QuestDone", menuName = "KiCo/Decisions/QuestDone")]
public class QuestDone : Decision
{
    public override bool Decide(Controller controller)
    {
        return LevelManager.instance.questIsComplete;
    }
}
