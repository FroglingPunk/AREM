public class HealthBar : BarBase
{
    private Entity _entity;


    public override void Init(Entity entity)
    {
        _entity = entity;

        _entity.HealthPoints.CurrentValue.ValueChanged += OnHealthPointsChanged;
        _entity.HealthPoints.MaxValue.ValueChanged += OnHealthPointsChanged;

        UpdateView(_entity.HealthPoints.CurrentValue.Value, _entity.HealthPoints.MaxValue.Value);
    }

    public override void Deinit()
    {
        _entity.HealthPoints.CurrentValue.ValueChanged -= OnHealthPointsChanged;
        _entity.HealthPoints.MaxValue.ValueChanged -= OnHealthPointsChanged;
    }


    private void OnHealthPointsChanged(int value)
    {
        UpdateView(_entity.HealthPoints.CurrentValue.Value, _entity.HealthPoints.MaxValue.Value);
    }
}