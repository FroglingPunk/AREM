using UnityEngine;

public abstract class MonoControllerBase : MonoBehaviour, IController
{
    protected virtual void Awake()
    {
        InternalInit();
    }

    protected virtual void OnDestroy()
    {
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