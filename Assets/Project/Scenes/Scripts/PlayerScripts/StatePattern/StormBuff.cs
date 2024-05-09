using UnityEngine;

public class StormBuff : StatePlayer
{
    public StormBuff(GameObject gameObject) : base(gameObject) {}
    

    public override void Enter()
    {
        animator.SetBool("StormBuff", true);
    }

    public override void Exit()
    {
        animator.SetBool("StormBuff", false);
    }
}
