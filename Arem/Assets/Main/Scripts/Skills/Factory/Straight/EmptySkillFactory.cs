public class EmptySkillFactory : SkillFactoryBase
{
    protected override void OnActivate()
    {
        ExecuteSkill();
    }

    protected override void OnDeactivate()
    {
    }


    protected override void MarkPossibleTargets()
    {
    }
}