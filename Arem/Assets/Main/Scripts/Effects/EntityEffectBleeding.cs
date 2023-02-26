public class EntityEffectBleeding : EntityEffectBase
{
    private int _damage;


    public EntityEffectBleeding(Entity entity, int duration, int damagePerTurn) :
        base(entity, new EntityEffectUpdaterEveryTurn(), duration)
    {
        _damage = damagePerTurn;
    }


    protected override void Update()
    {
        _entity.GetDamage(_damage);
    }
}