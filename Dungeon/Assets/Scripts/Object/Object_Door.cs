using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEngine.EventSystems.EventTrigger;

public class Object_Door : Object_Interactable
{
    private Animator anim;
    private PlayableDirector director;
    private bool opened = false;
    private BoxCollider2D solidCollider;

    [SerializeField] private float walkDistance = 2f;
    [SerializeField] private int requiredKey;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        director = GetComponentInChildren<PlayableDirector>();
        solidCollider = GetComponent<BoxCollider2D>();
    }
    public override void Interact()
    {
        if (opened) return;
        if (!player.HasKey(requiredKey)) return;

        opened = true;
        DestroyTooltip();

        StartCoroutine(OpenDoorCo());
    }

    private IEnumerator OpenDoorCo()
    {
        player.input.Player.Disable();
        anim.SetBool("open", opened);
        solidCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);

        Vector2 target = player.transform.position + Vector3.up * walkDistance;
        while (Vector2.Distance(player.transform.position, target) > 0.05f)
        {
            Vector2 direction = (target - (Vector2)player.transform.position).normalized;
            player.MoveDirection = direction;
            player.transform.position = Vector2.MoveTowards(player.transform.position, target, player.moveSpeed * Time.deltaTime);

            yield return null;
        }

        player.MoveDirection = Vector2.zero;
        director.Play();
        yield return new WaitUntil(() => director.state != PlayState.Playing);

        player.input.Player.Enable();
    }
}
