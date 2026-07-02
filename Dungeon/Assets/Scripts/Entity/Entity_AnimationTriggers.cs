using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity entity;

    private void Start()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void CurrentStateTrigger()
    {
        entity.stateMachine.currentState.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        entity.PerformAttack();
    }
}
