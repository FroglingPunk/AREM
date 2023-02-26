using UnityEngine;

public abstract class BarBase : MonoBehaviour
{
    [SerializeField] private Transform _frontSideTransform;


    public abstract void Init(Entity entity);

    public abstract void Deinit();


    protected void UpdateView(int currentValue, int maxValue)
    {
        var scale = _frontSideTransform.localScale;
        scale.x = (float)currentValue / maxValue;
        scale.x = Mathf.Clamp(scale.x, 0f, 1f);
        _frontSideTransform.localScale = scale;
    }
}