using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CurrentTurnFieldCellMark : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        var turnController = this.GetController<TurnController>();
        turnController.CurrentTurnEntity.ValueChanged += OnCurrentTurnEntityChanged;

        var battleManager = this.GetController<BattleManager>();
        battleManager.State.ValueChanged += OnBattleStateChanged;
    }


    private void OnCurrentTurnEntityChanged(Entity entity)
    {
        var cell = entity.FieldCell;

        transform.SetParent(cell.transform, true);
        transform.localPosition = new Vector3(0f, -0.6f, 0f);
    }

    private void OnBattleStateChanged(EBattleState state)
    {
        var isActive = state == EBattleState.WaitingForEnemyAIAction || state == EBattleState.WaitingForPlayerAction;
        _spriteRenderer.enabled = isActive;
    }
}