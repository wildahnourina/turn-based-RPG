using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity : MonoBehaviour
{
    public Entity_Health entityHealth { get; protected set; }
    public Entity_Stats stats { get; protected set; }

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }

    public StateMachine stateMachine;
    public float moveSpeed;
    public Vector2 MoveDirection { get; set; }//untuk arah battle

    protected virtual void Awake()
    {
        entityHealth = GetComponent<Entity_Health>();
        stats = GetComponent<Entity_Stats>();

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }

    public virtual void PerformAttack()
    {
        Entity target = BattleManager.instance.GetTarget(this);
        float damage = stats.CalculateDamage(target.stats);
        target.entityHealth.TakeDamage(damage);
    }
}
