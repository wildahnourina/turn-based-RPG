using UnityEngine;

public class Object_Chest : Object_Interactable
{
    private bool opened;
    private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
    }

    public override void Interact()
    {
        if (opened)
            return;

        opened = true;
        anim.SetTrigger("open");
        //kasih item
        DisableTooltip();
    }
}
