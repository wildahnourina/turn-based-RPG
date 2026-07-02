using UnityEngine;

public abstract class Object_Interactable : MonoBehaviour, IInteractable
{
    //protected Transform player;
    protected Player player;
    [SerializeField] private GameObject interactToolTip;

    protected virtual void Awake()
    {
        interactToolTip.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player == null)
            return;
        player.currentInteractable = this;

        interactToolTip.SetActive(true);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null && player.currentInteractable == this)
            player.currentInteractable = null;

        interactToolTip.SetActive(false);
    }

    protected void DisableTooltip()
    {
        interactToolTip.SetActive(false);
    }

    public abstract void Interact();
}
