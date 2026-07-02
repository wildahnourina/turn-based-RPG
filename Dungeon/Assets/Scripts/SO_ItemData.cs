using NUnit.Framework.Interfaces;
using UnityEngine;

[CreateAssetMenu(menuName = "Turn-based Setup/Item Data", fileName = "Item data - ")]
public class SO_ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public RuntimeAnimatorController animatorController;

    public ItemEffectType effectType;
    public float value;
    public  void Apply(Player player)
    {
        switch (effectType)
        {
            case ItemEffectType.Key:
                player.AddKey((int)value);
                break;

            case ItemEffectType.Heal:
                player.entityHealth.IncreaseHealth(value);
                break;

            case ItemEffectType.Attack:
                player.stats.IncreaseAttack(value);
                break;

            case ItemEffectType.Defense:
                player.stats.IncreaseDefense(value);
                break;
        }
    }
}

public enum ItemEffectType
{
    Key,
    Heal,
    Attack,
    Defense
}
