using UnityEngine;

public abstract class SelectableBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public T Selected { get; private set; }


    protected virtual void Awake()
    {
        Selected = GetComponent<T>();
    }

    protected virtual void Select()
    {
        this.GetController<MessageBus>().Callback(new SelectMessage<T>(Selected));
    }
}