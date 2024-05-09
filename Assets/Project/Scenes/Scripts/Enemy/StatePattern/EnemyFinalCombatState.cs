using UnityEngine;

public class EnemyFinalCombatState : StateEnemy
{
    public EnemyFinalCombatState(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("EnemyFinalCombat", true);
    }

    public override void Exit()
    {
        animator.SetBool("EnemyFinalCombat", false);
    }
}
