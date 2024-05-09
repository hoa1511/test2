using UnityEngine;

public class IdlePlayerState : StatePlayer
{
    public IdlePlayerState(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("Idle", true);
    }

    public override void Exit()
    {
        animator.SetBool("Idle", false);
    }
}
