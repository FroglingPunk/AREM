using System;

public class CallbackVariable<T>
{
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            ValueChanged?.Invoke(_value);
        }
    }

    public event Action<T> ValueChanged;


    public CallbackVariable(T value = default)
    {
        _value = value;
    }
}