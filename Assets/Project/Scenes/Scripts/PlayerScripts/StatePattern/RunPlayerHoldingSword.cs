using UnityEngine;

public class RunPlayerHoldingSword : StatePlayer
{
    public RunPlayerHoldingSword(GameObject gameObject) : base(gameObject) {}
    

    public override void Enter()
    {
        animator.SetBool("RunHoldingSword", true);
    }

    public override void Exit()
    {
        animator.SetBool("RunHoldingSword", false);
    }
}
