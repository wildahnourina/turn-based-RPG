using UnityEngine;

public class Enemy_MoveState : EnemyState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.MoveDirection == Vector2.zero)
            stateMachine.ChangeState(enemy.idleState);
    }
}
