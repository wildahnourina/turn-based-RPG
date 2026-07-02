using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState; //{get; private set;}
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public EnemyAction ChooseAction()
    {
        float hpPercent = (float)entityHealth.GetCurrentHealth() / stats.maxHealth;

        if (hpPercent <= 0.3f)
        {
            if (Random.value < 0.5f)
                return EnemyAction.Heal;
        }

        return EnemyAction.Attack;
    }
}

public enum EnemyAction
{
    Attack,
    Heal,
    Defense
}
