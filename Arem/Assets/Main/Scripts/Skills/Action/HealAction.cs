using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HealAction", menuName = "Skills/Actions/Heal", order = 0)]
public class HealAction : SkillActionBase
{
    public override IEnumerator Execute(SkillExecutionContext context)
    {
        context.Target.GetDamage(1);

        var a = context.Target.transform.position;
        var b = a + Vector3.up;

        for (float lerp = 0f; lerp < 1f; lerp += Time.deltaTime)
        {
            yield return null;
            context.Target.transform.position = Vector3.Lerp(a, b, lerp);
        }

        for (float lerp = 0f; lerp < 1f; lerp += Time.deltaTime)
        {
            yield return null;
            context.Target.transform.position = Vector3.Lerp(b, a, lerp);
        }

        context.Target.transform.position = a;

        yield return null;
    }
}