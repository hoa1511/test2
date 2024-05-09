using UnityEngine;

public class EnemyWalkState : StateEnemy
{
    public EnemyWalkState(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("EnemyWalking", true);
    }

    public override void Exit()
    {
        animator.SetBool("EnemyWalking", false);
    }
}
