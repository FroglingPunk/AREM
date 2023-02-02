using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillActionBase : ScriptableObject
{
    public abstract IEnumerator Execute(SkillExecutionContext context);
}