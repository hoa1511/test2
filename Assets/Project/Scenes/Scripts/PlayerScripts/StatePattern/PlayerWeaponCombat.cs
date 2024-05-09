using UnityEngine;

public class PlayerWeaponCombat : StatePlayer
{
    public PlayerWeaponCombat(GameObject gameObject) : base(gameObject){}

    public override void Enter()
    {
        animator.SetBool("WeaponCb", true);
    }

    public override void Exit()
    {
        animator.SetBool("WeaponCb", false);
    }
}
