public abstract class SkillFactoryBase
{
    protected SkillExecutionContext _context;


    public void Activate(SkillExecutionContext context)
    {
        _context = context;

        MarkPossibleTargets();
        OnActivate();
    }

    public void Deactivate()
    {
        OnDeactivate();
        UnmarkPossibleTargets();
    }

    protected void ExecuteSkill()
    {
        _context.Skill.Execute(_context);
    }


    protected abstract void MarkPossibleTargets();
    protected virtual void UnmarkPossibleTargets()
    {
        Field.Instance.ActionForAllCells((cell) => cell.Mark(ECellMarkState.Inactive));
    }

    protected abstract void OnActivate();
    protected abstract void OnDeactivate();
}