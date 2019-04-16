using UnityEngine;
using System.Collections;

public class WalkAnimation
{
    static int walk_hash = Animator.StringToHash("Walk");
    public static void SetState(Animator animator)
    {
        animator.Play(walk_hash);
    }
}
