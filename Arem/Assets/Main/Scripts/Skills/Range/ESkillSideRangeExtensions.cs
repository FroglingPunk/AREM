public static class ESkillSideRangeExtensions
{
    public static EFieldSide[] ConvertToFieldSides(this ESkillSideRange skillSideRange, EFieldSide skillSourceSide)
    {
        switch (skillSideRange)
        {
            case ESkillSideRange.Allies:
                return new EFieldSide[] { skillSourceSide };

            case ESkillSideRange.Enemy:
                return new EFieldSide[] { skillSourceSide.Opposite() };

            case ESkillSideRange.Both:
                return new EFieldSide[] { skillSourceSide, skillSourceSide.Opposite() };

            default:
                return default;
        }
    }
}