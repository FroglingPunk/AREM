public class SelfSkillRange : SkillRange
{
    public override void MarkPossibleTargets(SkillExecutionContext context)
    {
        context.Source.FieldCell.Mark(ECellMarkState.PossibleForAction);
    }
}