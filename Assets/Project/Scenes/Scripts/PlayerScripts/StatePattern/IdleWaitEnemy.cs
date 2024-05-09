using UnityEngine;

public class IdleWaitEnemy : StatePlayer
{
    public IdleWaitEnemy(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("IdleWaitEnemy", true);
    }

    public override void Exit()
    {
        animator.SetBool("IdleWaitEnemy", false);
    }
}

