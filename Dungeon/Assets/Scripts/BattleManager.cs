using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum BattleTurn
{
    Player,
    Enemy,
    Busy
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [SerializeField] private float enemySpawnDistance = 4f;
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private GameObject actionPanel;

    private BattleTurn currentTurn;

    private Player player;
    private Enemy enemy;

    private Vector2 playerStartPos;
    private Vector2 enemyStartPos;

    private EnemyEncounter currentEncounter;

    private void Awake()
    {
        instance = this;
    }
    private void SetTurn(BattleTurn turn)
    {
        currentTurn = turn;
        actionPanel.SetActive(turn == BattleTurn.Player);
    }


    public void StartBattle(GameObject enemyPrefab, EnemyEncounter encounter)
    {
        currentEncounter = encounter;

        player = FindFirstObjectByType<Player>();
        player.input.Player.Disable();
        playerStartPos = player.transform.position;
        StartCoroutine(FacingTarget(player, Vector2.right));

        enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        enemy.transform.position = player.transform.position + Vector3.right * enemySpawnDistance;
        enemyStartPos = enemy.transform.position;

        StartCoroutine(FacingTarget(enemy, Vector2.left));
        SetTurn(BattleTurn.Player);
    }

    private IEnumerator FacingTarget(Entity entity, Vector2 direction)
    {
        entity.MoveDirection = direction;
        yield return null;
        entity.MoveDirection = Vector2.zero;
    }

    public void OnAttackButtonPressed()
    {
        if (currentTurn != BattleTurn.Player) return;
        StartCoroutine(PlayerAttackCo());
    }

    public void OnHealButtonPressed()
    {
        if (currentTurn != BattleTurn.Player) return;
        player.entityHealth.IncreaseHealth(player.stats.heal);
        StartCoroutine(EnemyTurnRoutine());
    }

    public void OnDefenseButtonPressed()
    {
        if (currentTurn != BattleTurn.Player) return;
        player.stats.isDefending = true;
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator PlayerAttackCo()
    {
        SetTurn(BattleTurn.Busy);
        Vector2 attackPos = enemy.transform.position + Vector3.left * attackDistance;

        yield return MoveEntity(player,attackPos);
        player.stateMachine.ChangeState(player.attackState);
        yield return new WaitUntil(() => player.stateMachine.currentState != player.attackState);
        yield return MoveEntity(player, playerStartPos);
        yield return FacingTarget(player, Vector2.right);

        if (enemy.entityHealth.isDead)
        {
            StartCoroutine(PlayerWinRoutine());
            yield break;
        }

        yield return EnemyTurnRoutine();
    }

    private IEnumerator EnemyTurnRoutine()
    {
        SetTurn(BattleTurn.Enemy);
        yield return new WaitForSeconds (2f);

        EnemyAction action = enemy.ChooseAction();
        if (action == EnemyAction.Heal)
        {
            enemy.entityHealth.IncreaseHealth(enemy.stats.heal);
            yield return new WaitForSeconds(.5f);
            SetTurn(BattleTurn.Player);

            yield break;
        }
        if (action == EnemyAction.Defense)
        {
            enemy.stats.isDefending = true;
            yield return new WaitForSeconds(.5f);
            SetTurn(BattleTurn.Player);

            yield break;
        }

        Vector2 attackPos = player.transform.position + Vector3.right * attackDistance;

        yield return MoveEntity(enemy, attackPos);
        enemy.stateMachine.ChangeState(enemy.attackState);
        yield return new WaitUntil(() => enemy.stateMachine.currentState != enemy.attackState);
        yield return MoveEntity(enemy, enemyStartPos);
        yield return FacingTarget(enemy, Vector2.left);

        if (player.entityHealth.isDead)
        {
            StartCoroutine(PlayerLoseRoutine());
            yield break;
        }

        SetTurn(BattleTurn.Player);
    }

    private IEnumerator MoveEntity(Entity entity, Vector2 target)
    {
        while (Vector2.Distance(entity.transform.position, target) > 0.05f)
        {
            Vector2 direction = (target - (Vector2)entity.transform.position).normalized;

            entity.MoveDirection = direction;
            entity.transform.position = Vector2.MoveTowards(entity.transform.position, target, entity.moveSpeed * Time.deltaTime);

            yield return null;
        }

        entity.transform.position = target;
        entity.MoveDirection = Vector2.zero;
    }

    private IEnumerator PlayerWinRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(enemy.gameObject);
        currentEncounter.DropRewardItems();
        Destroy(currentEncounter.gameObject);

        EndBattle();
    }

    private IEnumerator PlayerLoseRoutine()
    {
        SetTurn(BattleTurn.Busy);

        yield return new WaitForSeconds(0.5f);

        player.entityHealth.RestoreFullHealth();

        Destroy(enemy.gameObject);

        EndBattle();
    }

    public void EndBattle()
    {
        SetTurn(BattleTurn.Busy);
        player.input.Player.Enable();
        AreaManager.instance.ReturnToSavedPoint(player.transform);

        enemy = null;
        currentEncounter = null;
    }

    public void ExitBattle()
    {
        player.entityHealth.RestoreFullHealth();
        Destroy(enemy.gameObject);
        EndBattle();
    }

    public Entity GetTarget(Entity attacker)
    {
        if (attacker == player)
            return enemy;

        return player;
    }

    public void Tes()
    {
        EndBattle();
    }
}

