using UnityEngine;

public abstract class Object_Interactable : MonoBehaviour, IInteractable
{
    //protected Transform player;
    protected Player player;
    [SerializeField] private GameObject interactToolTip;

    protected virtual void Awake()
    {
        if (interactToolTip != null) interactToolTip.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player == null)
            return;
        player.currentInteractable = this;

        if (interactToolTip != null) interactToolTip.SetActive(true);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null && player.currentInteractable == this)
            player.currentInteractable = null;

        if (interactToolTip != null) interactToolTip.SetActive(false);
    }

    protected void DestroyTooltip()
    {
        Destroy(interactToolTip);
    }

    public abstract void Interact();
}
