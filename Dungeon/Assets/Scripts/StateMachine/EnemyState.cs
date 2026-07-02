using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        anim = enemy.anim;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        if (enemy.MoveDirection != Vector2.zero)
        {
            anim.SetFloat("lastX", enemy.MoveDirection.x);
        }
    }
}
