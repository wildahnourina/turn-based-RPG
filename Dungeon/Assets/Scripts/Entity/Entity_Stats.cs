using UnityEditor;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public float maxHealth = 100;
    public float attack = 20;
    public float defense = 10;
    public float heal = 10;

    [HideInInspector] public bool isDefending;//dipake di TakeDamage()

    public float CalculateDamage(Entity_Stats target)
    {
        float damage = attack - target.defense;
        return Mathf.Max(1, damage);
    }

    public void IncreaseAttack(float value) => attack += value;
    public void IncreaseDefense(float value) => defense += value;
}
