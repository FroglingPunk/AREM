using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionStatesPanel : MonoBehaviour
{
    [SerializeField] private Transform _elementsParent;
    [SerializeField] private ActionStateView _elementPrefab;

    public event Action<ActionState> OnElementClicked;
    public event Action<ActionState> OnElementDoubleClicked;
    public event Action<ActionState> OnElementPressed;
    public event Action<ActionState> OnElementMouseExit;
    public event Action<ActionState> OnElementMouseEnter;
    public event Action<ActionState> OnElementDestroyButtonClick;

    private List<ActionStateView> _elements = new List<ActionStateView>();


    public void Show(List<ActionState> states)
    {
        Clear();

        for (var i = 0; i < states.Count; i++)
        {
            var element = Instantiate(_elementPrefab, _elementsParent);
            var state = states[i];

            element.Init(state);
            element.MouseHandler.OnClickCallback += () => OnElementClicked?.Invoke(state);
            element.MouseHandler.OnDoubleClickCallback += () => OnElementDoubleClicked?.Invoke(state);
            element.MouseHandler.OnPressedCallback += () => OnElementPressed?.Invoke(state);
            element.MouseHandler.OnMouseExitCallback += () => OnElementMouseExit?.Invoke(state);
            element.MouseHandler.OnMouseEnterCallback += () => OnElementMouseEnter?.Invoke(state);
            element.OnButtonDestroyClick += () => OnElementDestroyButtonClick?.Invoke(state);

            _elements.Add(element);
        }
    }

    public void Clear()
    {
        _elements.ForEach((element) => Destroy(element.gameObject));
        _elements.Clear();
    }

    public ActionStateView GetActionStateView(ActionState state)
    {
        return _elements.Find((view) => view.ActionState == state);
    }
}