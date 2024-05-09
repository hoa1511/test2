using UnityEngine;

public class DiePlayerByTrap : StatePlayer
{
    public DiePlayerByTrap(GameObject gameObject) : base(gameObject) {}
    

    public override void Enter()
    {
        animator.SetBool("Die", true);
    }

    public override void Exit()
    {
        animator.SetBool("Die", false);
    }
}
