using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionStateView : MonoBehaviour
{
    [SerializeField] protected Button _buttonDestroy;
    [SerializeField] protected MouseHandler _mouseHandler;
    [SerializeField] protected Text _textActionState;

    public MouseHandler MouseHandler => _mouseHandler;
    public event Action OnButtonDestroyClick;

    public ActionState ActionState { get; private set; }

    private Color _defaultColor;


    private void Awake()
    {
        _defaultColor = GetComponent<Image>().color;
    }


    public virtual void Init(ActionState actionState)
    {
        ActionState = actionState;

        _textActionState.text = actionState.Description;

        if (_buttonDestroy)
        {
            _buttonDestroy.onClick.RemoveAllListeners();
            _buttonDestroy.onClick.AddListener(() => OnButtonDestroyClick?.Invoke());
        }
    }


    public void SetColor(Color color)
    {
        GetComponent<Image>().color = color;
    }

    public void SetDefaultColor()
    {
        SetColor(_defaultColor);
    }
}