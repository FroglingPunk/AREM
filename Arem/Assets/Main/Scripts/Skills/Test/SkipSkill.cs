using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SkipSkill", menuName = "Skills/SkipSkill", order = 0)]
public class SkipSkill : StraightSkillBase
{
    private SkillFactoryBase _factory = new EmptySkillFactory();
    public override SkillFactoryBase Factory => _factory;
    public override SkillRange Range => throw new System.NotImplementedException();


    protected override IEnumerator InternalExecuting(SkillExecutionContext context)
    {
        yield return null;
    }
}