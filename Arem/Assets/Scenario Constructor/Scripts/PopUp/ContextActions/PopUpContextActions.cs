using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class PopUpContextActions : PopUpViewBase<PopUpContextActionsContextData>
{
    [SerializeField] private ContextActionButton _buttonPrefab;

    private GridLayoutGroup _grid;

    private Queue<ContextActionButton> _pool = new Queue<ContextActionButton>();
    private List<ContextActionButton> _activeButtons = new List<ContextActionButton>();


    private void Awake()
    {
        _grid = GetComponent<GridLayoutGroup>();
    }


    protected override void InternalShow(PopUpContextActionsContextData contextData)
    {
        Clear();

        transform.position = contextData.Position;

        for (var i = 0; i < contextData.ActionsData.Length; i++)
        {
            var actionData = contextData.ActionsData[i];
            var button = GetButton();

            button.Init(actionData.Name, actionData.Callback + Hide);
        }

        var buttonHeight = (_buttonPrefab.transform as RectTransform).sizeDelta.y;
        var popUpHeight = _activeButtons.Count * buttonHeight;

        if (contextData.Position.y - popUpHeight < 0)
            _grid.childAlignment = TextAnchor.LowerCenter;
        else
            _grid.childAlignment = TextAnchor.UpperCenter;
    }

    protected override void InternalHide()
    {
        Clear();
    }


    private void Clear()
    {
        for (var i = _activeButtons.Count - 1; i >= 0; i--)
            ReturnButtonToPool(_activeButtons[i]);
    }

    private ContextActionButton GetButton()
    {
        var button = (ContextActionButton)default;

        if (_pool.Count > 0)
            button = _pool.Dequeue();
        else
            button = Instantiate(_buttonPrefab, transform);

        button.transform.SetSiblingIndex(_activeButtons.Count);
        _activeButtons.Add(button);
        return button;
    }

    private void ReturnButtonToPool(ContextActionButton button)
    {
        button.Deinit();
        _activeButtons.Remove(button);
        _pool.Enqueue(button);
    }
}