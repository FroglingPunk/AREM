using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TestSkill", menuName = "Skills/TestSkill", order = 0)]
public class TestSkill : StraightSkillBase
{
    private SkillFactoryBase _factory = new StraightSkillFactory();
    public override SkillFactoryBase Factory => _factory;
    public override SkillRange Range => throw new System.NotImplementedException();


    protected override IEnumerator InternalExecuting(SkillExecutionContext context)
    {
        context.Target.GetDamage(1);

        var a = context.Source.transform.position;
        var b = a + Vector3.up;

        for(float lerp = 0f; lerp < 1f; lerp += Time.deltaTime)
        {
            yield return null;
            context.Source.transform.position = Vector3.Lerp(a, b, lerp);
        }

        for (float lerp = 0f; lerp < 1f; lerp += Time.deltaTime)
        {
            yield return null;
            context.Source.transform.position = Vector3.Lerp(b, a, lerp);
        }

        context.Source.transform.position = a;

        yield return null;
    }
}