using UnityEngine;

[RequireComponent(typeof(SkillButton))]
public class SelectableSkillButton : SelectableButton<SkillButton>
{
    protected override void Select()
    {
        this.GetController<MessageBus>().Callback(new SelectMessage<Skill>(Selected.Skill));
    }
}