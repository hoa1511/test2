using UnityEngine;

public class WalkPlayerState : StatePlayer
{
    public WalkPlayerState(GameObject gameObject) : base(gameObject) {}
    

    public override void Enter()
    {
        animator.SetBool("Die", true);
    }

    public override void Exit()
    {
        animator.SetBool("Die", true);
    }
}
