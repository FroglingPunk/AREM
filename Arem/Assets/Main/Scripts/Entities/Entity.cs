using UnityEngine;

public class Entity : MonoBehaviour
{
    protected EntityData _data;

    public EntityStats Stats => _data.Stats;
    public Skill[] Skills => _data.GameParams.Skills;
    public ETeam Team => _data.Team;
    public FieldCell FieldCell { get; protected set; }
    public EntityHealthPoints HealthPoints { get; protected set; }


    protected virtual void Start()
    {
        this.GetController<MessageBus>().Callback(new CreateMessage<Entity>(this));

        HealthPoints = new EntityHealthPoints();
        HealthPoints.MaxValue.Value = 10;
        HealthPoints.CurrentValue.Value = 10;

        var bars = GetComponentsInChildren<BarBase>();
        for (var i = 0; i < bars.Length; i++)
            bars[i].Init(this);
    }

    protected virtual void OnDestroy()
    {
        this.GetController<MessageBus>().Callback(new DestroyMessage<Entity>(this));
    }


    public void GetDamage(int damage)
    {
        Debug.Log($"Get {damage} damage to {gameObject.name} ");

        var previousHP = HealthPoints.CurrentValue.Value;

        HealthPoints.CurrentValue.Value -= damage;

        this.GetController<MessageBus>().Callback(new EntityHPChangedMessage(this, previousHP, HealthPoints.CurrentValue.Value));
    }


    public virtual void Init(EntityData data)
    {
        _data = data;

        FieldCell = Field.Instance[data.FieldPosition];
        FieldCell.EntityOnPosition = this;
        transform.position = FieldCell.transform.position;
    }
}