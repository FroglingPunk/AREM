using System;

public class EntityEffectUpdaterEveryTurn : EntityEffectUpdaterBase
{
    public override event Action Updated;


    public override void Init()
    {
        var turnController = this.GetController<TurnController>();
        turnController.Turn.ValueChanged += OnTurnChanged;
    }

    public override void Deinit()
    {
        var turnController = this.GetController<TurnController>();
        turnController.Turn.ValueChanged -= OnTurnChanged;
    }


    private void OnTurnChanged(int turn)
    {
        Updated?.Invoke();
    }
}