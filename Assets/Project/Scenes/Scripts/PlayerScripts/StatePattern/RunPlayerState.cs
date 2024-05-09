using UnityEngine;

public class RunPlayerState : StatePlayer
{
    public RunPlayerState(GameObject gameObject) : base(gameObject) {}
    

    public override void Enter()
    {
        animator.SetBool("Run", true);
    }

    public override void Exit()
    {
        animator.SetBool("Run", false);
    }
}
