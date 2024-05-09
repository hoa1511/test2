using UnityEngine;

public abstract class StateEnemy: MonoBehaviour
{
    protected GameObject gameObj;
    protected Animator animator;

    public StateEnemy(GameObject gameObject)
    {
        this.gameObj = gameObject;
        animator = gameObject.GetComponent<Animator>();
    }

    public abstract void Enter();
    public abstract void Exit();
}

