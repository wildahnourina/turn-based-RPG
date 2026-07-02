using NUnit.Framework.Interfaces;
using UnityEngine;

public class Object_ItemPickup : MonoBehaviour
{
    [SerializeField] private SO_ItemData itemData;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;

    private void OnValidate()
    {
        if (itemData == null)
            return;

        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        SetupVisual();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        itemData.Apply(player);
        Destroy(gameObject);
    }

    public void SetupItem(SO_ItemData itemData)
    {
        this.itemData = itemData;
        SetupVisual();
        anim.runtimeAnimatorController = itemData.animatorController;
    }

    private void SetupVisual()
    {
        sr.sprite = itemData.itemIcon;
        gameObject.name = "Object_ItemPickup - " + itemData.itemName;
    }

}
