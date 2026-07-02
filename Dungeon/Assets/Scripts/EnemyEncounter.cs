using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [Header("Battle")]
    [SerializeField] private GameObject battleEnemyPrefab;
    [SerializeField] private Area battleArea;
    [SerializeField] private float returnOffset = 1.5f;

    [Header("Reward")]
    [SerializeField] private GameObject itemPickupPrefab;
    [SerializeField] private List<SO_ItemData> rewardItems;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Player player))
            return;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 offset;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            offset = direction.x > 0 ? Vector2.right : Vector2.left;
        else
            offset = direction.y > 0 ? Vector2.up : Vector2.down;

        Vector2 returnPosition = (Vector2)player.transform.position + offset * returnOffset;
        AreaManager.instance.SaveReturnPoint(returnPosition);

        AreaManager.instance.EnterArea(battleArea, player.transform, battleArea.playerSpawnPoint.position);
        BattleManager.instance.StartBattle(battleEnemyPrefab, this);
    }

    public void DropRewardItems()
    {
        foreach (SO_ItemData item in rewardItems)
        {
            Vector2 offset = Random.insideUnitCircle * 0.8f;

            GameObject pickup = Instantiate(
                itemPickupPrefab,
                (Vector2)transform.position + offset,
                Quaternion.identity);

            pickup.GetComponent<Object_ItemPickup>().SetupItem(item);
        }
    }
}
