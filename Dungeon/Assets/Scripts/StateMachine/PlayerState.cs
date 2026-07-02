using UnityEngine;

public class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        if (player.moveInput != Vector2.zero)
        {
            anim.SetFloat("lastX", player.moveInput.x);
            anim.SetFloat("lastY", player.moveInput.y);
        }

        if (player.MoveDirection != Vector2.zero)
        {
            anim.SetFloat("lastX", player.MoveDirection.x);
            anim.SetFloat("lastY", player.MoveDirection.y);
        }
    }
} 