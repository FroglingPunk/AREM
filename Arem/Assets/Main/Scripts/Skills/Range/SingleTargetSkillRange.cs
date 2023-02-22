public class SingleTargetSkillRange : SkillRange
{
    public ESkillSideRange Side;
    public EFieldLevel[] PossibleLevels;
    public EFieldLinePosition[] PossiblePositions;

    public override void MarkPossibleTargets(SkillExecutionContext context)
    {
        var field = Field.Instance;
        var sides = Side.ConvertToFieldSides(context.Source.FieldCell.Index.Side);

        for (var sideID = 0; sideID < sides.Length; sideID++)
        {
            for (var levelID = 0; levelID < PossibleLevels.Length; levelID++)
            {
                for (var posID = 0; posID < PossiblePositions.Length; posID++)
                {
                    field[sides[sideID], PossibleLevels[levelID], PossiblePositions[posID]].Mark(ECellMarkState.PossibleForAction);
                }
            }
        }
    }
}