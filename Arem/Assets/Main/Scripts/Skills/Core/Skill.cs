using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [SerializeField] private ESkillType _skillType;
    [SerializeField] private string _audioAssetName;
    [SerializeField] protected Texture2D _sprite;
    [SerializeField] protected int _range;

    public CallbackVariable<ESkillState> State { get; private set; } = new CallbackVariable<ESkillState>(ESkillState.Inactive);
    public ESkillType SkillType => _skillType;
    public int Range => _range;
    public Texture2D Sprite => _sprite;

    public abstract SkillFactoryBase Factory { get; }


    public void Execute(SkillExecutionContext context)
    {
        CoroutinesRunner.Instance.RunCoroutine(ExecutionCoroutine(context));
    }

    private IEnumerator ExecutionCoroutine(SkillExecutionContext context)
    {
        Factory.Deactivate();

        State.Value = ESkillState.StartExecuting;
        //this.GetController<AudioPlayer>().PlayOneShot(_audioAssetName);
        yield return InternalExecuting(context);
        State.Value = ESkillState.FinishExecuting;
    }

    protected abstract IEnumerator InternalExecuting(SkillExecutionContext context);

    public virtual void OnSelected(SkillExecutionContext context)
    {
        State.Value = ESkillState.Active;
        Factory.Activate(context);
    }

    public virtual void OnDeselected()
    {
        State.Value = ESkillState.Inactive;
        Factory.Deactivate();
    }
}