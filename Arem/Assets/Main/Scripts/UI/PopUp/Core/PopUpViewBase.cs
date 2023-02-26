using UnityEngine;

public abstract class PopUpViewBase<T> : MonoBehaviour, IPopUpView where T : PopUpContextDataBase
{
    protected abstract void InternalShow(T contextData);
    protected abstract void InternalHide();


    public void Show(PopUpContextDataBase contextData)
    {
        if (!(contextData is T))
        {
            Debug.LogError($"Wrong context data for {GetType()} pop up");
            return;
        }

        gameObject.SetActive(true);
        InternalShow(contextData as T);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        InternalHide();
    }
}