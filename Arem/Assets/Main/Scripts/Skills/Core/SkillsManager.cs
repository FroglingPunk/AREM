using System;

public class SkillsManager : ControllerBase
{
    public CallbackVariable<Skill> SelectedSkill { get; private set; } = new CallbackVariable<Skill>();
    public event Action<Skill> OnSkillStartExecuting;
    public event Action<Skill> OnSkillFinishExecuting;


    protected override void InternalInit()
    {
        base.InternalInit();

        var messageBus = this.GetController<MessageBus>();
        messageBus.Subscribe<SelectMessage<Skill>>(OnSkillSelected);
    }


    private void OnSkillSelected(IMessage message)
    {
        if (SelectedSkill.Value)
        {
            SelectedSkill.Value.OnDeselected();
            SelectedSkill.Value.State.ValueChanged -= OnSelectedSkillStateChanged;
        }

        var skill = (message as SelectMessage<Skill>).Selected;

        if (skill == null)
            return;

        var turnController = this.GetController<TurnController>();
        var skillExecutionContext = new SkillExecutionContext { Skill = skill, Source = turnController.CurrentTurnEntity.Value };

        SelectedSkill.Value = skill;
        SelectedSkill.Value.OnSelected(skillExecutionContext);
        SelectedSkill.Value.State.ValueChanged += OnSelectedSkillStateChanged;
    }

    private void OnSelectedSkillStateChanged(ESkillState state)
    {
        if (state == ESkillState.StartExecuting)
        {
            OnSkillStartExecuting?.Invoke(SelectedSkill.Value);
        }
        else if (state == ESkillState.FinishExecuting)
        {
            OnSkillFinishExecuting?.Invoke(SelectedSkill.Value);
        }
    }
}