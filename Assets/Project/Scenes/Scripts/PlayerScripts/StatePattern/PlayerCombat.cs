using UnityEngine;

public class PlayerCombat : StatePlayer
{
    public PlayerCombat(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("HandCombat", true);
    }

    public override void Exit()
    {
        animator.SetBool("HandCombat", false);
    }
}
