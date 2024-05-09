using UnityEngine;

public class EnemyDance : StateEnemy
{
    public EnemyDance(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("EnemyDance", true);
    }

    public override void Exit()
    {
        animator.SetBool("EnemyDance", false);
    }
}