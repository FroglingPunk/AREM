using UnityEngine;

public class Entity : MonoBehaviour
{
    protected EntityData _data;

    public EntityStats Stats => _data.Stats;
    public Skill[] Skills => _data.GameParams.Skills;
    public ETeam Team => _data.Team;
    public FieldCell FieldCell { get; protected set; }


    protected virtual void Start()
    {
        this.GetController<MessageBus>().Callback(new CreateMessage<Entity>(this));
    }

    protected virtual void OnDestroy()
    {
        this.GetController<MessageBus>().Callback(new DestroyMessage<Entity>(this));
    }


    public void GetDamage(int damage)
    {

    }


    public virtual void Init(EntityData data)
    {
        _data = data;

        FieldCell = Field.Instance[data.FieldPosition];
        FieldCell.EntityOnPosition = this;
        transform.position = FieldCell.transform.position;
    }
}