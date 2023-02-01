using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

public class SkillsStorage
{
    private static List<Skill> _skills;

    public Skill this[ESkillType skillType] => _skills.Find((skill) => skill.SkillType == skillType);

    public static IEnumerator LoadPopUpPrefabs()
    {
        if (_skills != null)
            yield break;

        _skills = new List<Skill>();

        var handle = Addressables.LoadAssetsAsync<Skill>("Skill", _skills.Add);

        yield return handle;
    }
}