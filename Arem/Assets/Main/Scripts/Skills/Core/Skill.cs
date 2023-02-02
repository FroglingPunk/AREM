using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skills/Skill", order = 0)]
public class Skill : ScriptableObject
{
    [SerializeField] private ESkillType _skillType;

    //[SerializeField] private string _audioAssetName;
    [SerializeField] protected Texture2D _sprite;

    [SerializeField] private SkillFactoryBase _factory;
    [SerializeField] private SkillActionBase _action;
    [SerializeField] private SkillRange _range;

    public CallbackVariable<ESkillState> State { get; private set; } = new CallbackVariable<ESkillState>(ESkillState.Inactive);

    public ESkillType SkillType => _skillType;
    public Texture2D Sprite => _sprite;


    public void Execute(SkillExecutionContext context)
    {
        CoroutinesRunner.Instance.RunCoroutine(ExecutionCoroutine(context));
    }

    private IEnumerator ExecutionCoroutine(SkillExecutionContext context)
    {
        _factory.Deactivate();

        State.Value = ESkillState.StartExecuting;
        //this.GetController<AudioPlayer>().PlayOneShot(_audioAssetName);
        yield return _action.Execute(context);
        State.Value = ESkillState.FinishExecuting;
    }


    public virtual void OnSelected(SkillExecutionContext context)
    {
        context.Range = _range;

        State.Value = ESkillState.Active;
        _factory.Activate(context);
    }

    public virtual void OnDeselected()
    {
        State.Value = ESkillState.Inactive;
        _factory.Deactivate();
    }
}