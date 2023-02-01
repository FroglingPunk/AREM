public class BattleManager : ControllerBase
{
    private PlayerGameController _playerGameController;
    private EnemyAIGameController _enemyAIGameController;

    public CallbackVariable<EBattleState> State { get; private set; } = new CallbackVariable<EBattleState>(EBattleState.WaitingForBattleStart);


    protected override void InternalInit()
    {
        base.InternalInit();

        _playerGameController = new PlayerGameController();
        _enemyAIGameController = new EnemyAIGameController();
    }

    public void StartBattle()
    {
        var turnController = this.GetController<TurnController>();
        turnController.CurrentTurnEntity.ValueChanged += OnCurrentTurnEntityChanged;

        var skillsManager = this.GetController<SkillsManager>();
        skillsManager.OnSkillStartExecuting += OnSkillStartExecuting;
        skillsManager.OnSkillFinishExecuting += OnSkillFinishExecuting;

        turnController.StartFirstTurn();
    }

    private void OnCurrentTurnEntityChanged(Entity entity)
    {
        State.Value = entity.Team == ETeam.Player ? EBattleState.WaitingForPlayerAction : EBattleState.WaitingForEnemyAIAction;

        _playerGameController.TransferControl(entity.Team == ETeam.Player);
        _enemyAIGameController.TransferControl(entity.Team == ETeam.EnemyAI);
    }

    private void OnSkillStartExecuting(Skill skill)
    {
        State.Value = EBattleState.Action;

        _playerGameController.TransferControl(false);
        _enemyAIGameController.TransferControl(false);
    }

    private void OnSkillFinishExecuting(Skill skill)
    {
        var turnController = this.GetController<TurnController>();
        turnController.NextQueue();
    }
}

public enum EBattleState
{
    WaitingForBattleStart,
    WaitingForPlayerAction,
    WaitingForEnemyAIAction,
    Action,
    Finish
}