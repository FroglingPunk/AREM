public class DestroyMessage<T> : IMessage
{
    public readonly T Destroyed;


    public DestroyMessage(T destroyed)
    {
        Destroyed = destroyed;
    }
}

public class EntityHPChangedMessage : IMessage
{
    public readonly Entity Entity;

    public readonly int PreviousHP;
    public readonly int CurrentHP;

    public int Delta => CurrentHP - PreviousHP;


    public EntityHPChangedMessage(Entity entity, int previousHP, int currentHP)
    {
        Entity = entity;
        PreviousHP = previousHP;
        CurrentHP = currentHP;
    }
}