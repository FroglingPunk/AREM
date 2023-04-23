using System;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public event Action OnClickCallback;
    public event Action OnDoubleClickCallback;
    public event Action OnPressedCallback;
    public event Action OnMouseExitCallback;
    public event Action OnMouseEnterCallback;

    private Timer _doubleClickTimer;
    private Timer _pressTimer;


    private void Awake()
    {
        _doubleClickTimer = new Timer(300);
        _doubleClickTimer.AutoReset = false;
        _doubleClickTimer.Elapsed += (src, args) => _doubleClickTimer.Stop();

        _pressTimer = new Timer(1500);
        _pressTimer.AutoReset = false;
        _pressTimer.Elapsed += (src, args) => OnPressedCallback?.Invoke();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickCallback?.Invoke();

        if (_doubleClickTimer.Enabled)
        {
            OnDoubleClickCallback?.Invoke();
            return;
        }

        _doubleClickTimer.Start();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressTimer.Start();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressTimer.Stop();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitCallback?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterCallback?.Invoke();
    }
}