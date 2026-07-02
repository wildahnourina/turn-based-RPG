using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class EntityState
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        UpdateAnimationParameters();
    }

    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }

    public virtual void UpdateAnimationParameters() { }

    public void AnimationTrigger()
    {
        triggerCalled = true;
    }

}
