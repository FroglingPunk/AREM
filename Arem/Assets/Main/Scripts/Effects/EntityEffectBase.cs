public abstract class EntityEffectBase
{
    protected Entity _entity;

    private EntityEffectUpdaterBase _updater;
    private int _duration;
    private int _completedUpdatesCount;


    public EntityEffectBase(Entity entity, EntityEffectUpdaterBase updater, int duration)
    {
        _entity = entity;
        _updater = updater;
        _duration = duration;
    }


    public virtual void Init()
    {
        _updater.Updated += Update;
        _updater.Updated += AfterUpdateCheck;
        _updater.Init();
    }

    public virtual void Deinit()
    {
        _updater.Updated -= Update;
        _updater.Updated -= AfterUpdateCheck;
        _updater.Deinit();
    }


    protected abstract void Update();


    private void AfterUpdateCheck()
    {
        if (++_completedUpdatesCount < _duration)
            return;

        Deinit();
    }
}