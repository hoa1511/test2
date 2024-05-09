using UnityEngine;

public class EnemyDieState : StateEnemy
{
    public EnemyDieState(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        Debug.Log("???");
        animator.SetBool("EnemyDie", true);
    }

    public override void Exit()
    {
        animator.SetBool("EnemyDie", false);
    }
}
