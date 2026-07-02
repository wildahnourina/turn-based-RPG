using UnityEngine;

public class Player_MoveState : PlayerState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.moveInput * player.moveSpeed);

        if (player.moveInput == Vector2.zero && player.MoveDirection == Vector2.zero)
            stateMachine.ChangeState(player.idleState);
    }
}
