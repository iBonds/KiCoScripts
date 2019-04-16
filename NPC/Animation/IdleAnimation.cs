using UnityEngine;
using System.Collections;

public class IdleAnimation
{
    static int idle_hash = Animator.StringToHash("Idle");
    public static void SetState(Animator animator)
    {
        animator.Play(idle_hash);
    }
}
