using System;

public abstract class ControllerBase : IController, IInitializationRequirable, IDisposable
{
    public bool IsInit { get; set; }


    public void Init()
    {
        if (IsInit)
            return;

        InternalInit();
    }

    public void Dispose()
    {
        if (!IsInit)
            return;

        InternalDestroy();
    }

    protected virtual void InternalInit()
    {
        
    }

    protected virtual void InternalDestroy()
    {
        ControllersContainer.RemoveController(this);
    }
}