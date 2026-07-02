using UnityEngine;

public class Player_IdleState : PlayerState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput != Vector2.zero || player.MoveDirection != Vector2.zero)
            stateMachine.ChangeState(player.moveState);
    }
    
}
