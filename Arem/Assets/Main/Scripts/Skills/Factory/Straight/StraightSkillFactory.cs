public class StraightSkillFactory : SkillFactoryBase
{
    protected override void OnActivate()
    {
        var messageBus = this.GetController<MessageBus>();
        messageBus.Subscribe<SelectMessage<FieldCell>>(OnFieldCellSelected);
    }

    protected override void OnDeactivate()
    {
        var messageBus = this.GetController<MessageBus>();
        messageBus.Unsubscribe<SelectMessage<FieldCell>>(OnFieldCellSelected);
    }


    protected override void MarkPossibleTargets()
    {
        var source = this.GetController<TurnController>().CurrentTurnEntity.Value;
        var entitiesManager = this.GetController<EntitiesManager>();

        entitiesManager[source.Team.OppositeTeam()].ForEach((entity) =>
        {
            if (entity != _context.Source)
                entity.FieldCell.Mark(ECellMarkState.PossibleForAction);
        });
    }

    private void OnFieldCellSelected(IMessage msg)
    {
        var cell = (msg as SelectMessage<FieldCell>).Selected;

        if (cell.MarkState != ECellMarkState.PossibleForAction)
            return;

        _context.Target = cell.EntityOnPosition;

        ExecuteSkill();
    }
}