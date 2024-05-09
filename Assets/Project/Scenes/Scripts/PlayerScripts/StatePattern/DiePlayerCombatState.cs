using UnityEngine;

public class DiePlayerCombatState : StatePlayer
{
    public DiePlayerCombatState(GameObject gameObject) : base(gameObject){}


    public override void Enter()
    {
       animator.SetBool("DieByEnemy", true); 
    }

    public override void Exit()
    {
        animator.SetBool("DieByEnemy", false);
    }
}
