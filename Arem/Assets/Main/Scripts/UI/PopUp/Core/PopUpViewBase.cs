using System;
using UnityEngine;

public abstract class PopUpViewBase<T> : MonoBehaviour, IPopUpView where T : PopUpContextDataBase
{
    public event Action OnHide;

    protected T _contextData;

    protected abstract void InternalShow(T contextData);
    protected abstract void InternalHide();


    public void Show(PopUpContextDataBase contextData)
    {
        if (!(contextData is T) && contextData != null)
        {
            Debug.LogError($"Wrong context data for {GetType()} pop up");
            return;
        }

        _contextData = contextData as T;
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        InternalShow(contextData as T);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        InternalHide();

        OnHide?.Invoke();
        OnHide = null;
    }
}