using UnityEngine;

public class EnemyChasingState : StateEnemy
{
    public EnemyChasingState(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("EnemyRun", true);
    }

    public override void Exit()
    {
        animator.SetBool("EnemyRun", false);
    }
}
