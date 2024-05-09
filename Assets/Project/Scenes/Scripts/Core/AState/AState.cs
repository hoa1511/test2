using UnityEngine;

public abstract class StatePlayer: MonoBehaviour
{
    protected GameObject gameObj;
    protected Animator animator;

    public StatePlayer(GameObject gameObject)
    {
        this.gameObj = gameObject;
        animator = gameObject.GetComponent<Animator>();
    }

    public abstract void Enter();
    public abstract void Exit();
}
