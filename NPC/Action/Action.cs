using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject, IAnimationSwitcher
{
    public abstract void Act(Controller controller);
    public abstract void SetAnimation(Animator animator);
}
