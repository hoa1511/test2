using UnityEngine;

public class EnemyIdleState : StateEnemy
{
    public EnemyIdleState(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("EnemyIdle", true);
    }

    public override void Exit()
    {
        animator.SetBool("EnemyIdle", false);
    }
}

