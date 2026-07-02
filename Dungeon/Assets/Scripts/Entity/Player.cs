using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_AttackState attackState { get; private set; }

    public Vector2 moveInput { get; private set; }
    public Object_Interactable currentInteractable { get; set; }

    private List<int> keys = new List<int>();

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        attackState = new Player_AttackState(this, stateMachine, "attack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void AddKey(int keyID)
    {
        if (!keys.Contains(keyID))
            keys.Add(keyID);
    }

    public bool HasKey(int keyID)
    {
        return keys.Contains(keyID);
    }

    public void EnablePlayerInput() => input.Player.Enable();
    public void DisablePlayerInput() => input.Player.Disable();

    private void OnEnable()
    {
        input.Enable();

        //input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.performed += ctx =>
        {
            Vector2 move = ctx.ReadValue<Vector2>();

            if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
                move.y = 0;
            else
                move.x = 0;

            moveInput = move;

            MoveDirection = moveInput;
        };
        input.Player.Movement.canceled += ctx =>
        {
            moveInput = Vector2.zero;
            MoveDirection = Vector2.zero;
        };

        input.Player.Interact.performed += ctx => currentInteractable.Interact();
    }

    private void OnDisable() => input.Disable();

}
