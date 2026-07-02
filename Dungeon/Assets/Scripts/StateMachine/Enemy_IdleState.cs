using UnityEngine;

public class Enemy_IdleState : EnemyState
{
    public Enemy_IdleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.MoveDirection != Vector2.zero)
            stateMachine.ChangeState(enemy.moveState);
    }
}
