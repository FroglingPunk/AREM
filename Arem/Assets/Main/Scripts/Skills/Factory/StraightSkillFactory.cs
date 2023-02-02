using UnityEngine;

[CreateAssetMenu(fileName = "StraightSkillFactory", menuName = "Skills/Factories/Straight", order = 0)]
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
        var range = _context.Range;
        Debug.Log(range is null);
        Debug.Log(range.PossibleSides is null);
        for (int sideID = 0; sideID < range.PossibleSides.Length; sideID++)
        {
            for (int levelID = 0; levelID < range.PossibleLevels.Length; levelID++)
            {
                for (int posID = 0; posID < range.PossiblePositions.Length; posID++)
                {
                    Field.Instance[range.PossibleSides[sideID], range.PossibleLevels[levelID], range.PossiblePositions[posID]].Mark(ECellMarkState.PossibleForAction);
                }
            }
        }
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