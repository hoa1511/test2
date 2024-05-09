using UnityEngine;

public class RunSkillPlayer : StatePlayer
{
    public RunSkillPlayer(GameObject gameObject) : base(gameObject) {}
    

    public override void Enter()
    {
        animator.SetBool("FastRun", true);
    }

    public override void Exit()
    {
        animator.SetBool("FastRun", false);
    }
}