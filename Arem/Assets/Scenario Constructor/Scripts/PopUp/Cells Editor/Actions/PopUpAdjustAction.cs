using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpAdjustAction : PopUpViewBase<PopUpAdjustActionContextData>
{
    [SerializeField] private Text _textActionTimeAndActor;

    [SerializeField] private Button _buttonClose;
    [SerializeField] private Button _buttonAddActionState;
    [SerializeField] private Button _buttonAddActionCondition;

    [SerializeField] private ActionStatesPanel _currentActionStatesPanel;
    [SerializeField] private ActionStatesPanel _currentActionStateConditionsPanel;

    private ActionState _selectedActionState;


    protected override void InternalShow(PopUpAdjustActionContextData contextData)
    {
        Clear();

        var timeData = contextData.ActionData.Container.GetContent<TimeData>();
        var actorData = contextData.ActionData.Container.GetContent<ActorData>();
        _textActionTimeAndActor.text = $"{actorData} | {timeData}";

        _currentActionStatesPanel.Show(contextData.ActionData.States);

        _currentActionStatesPanel.OnElementClicked += OnCurrentActionStatePanelElementClicked;
        _currentActionStatesPanel.OnElementDestroyButtonClick += OnCurrentActionStatePanelElementDestroyButtonClick;
        _currentActionStatesPanel.OnElementDoubleClicked += OnCurrentActionStatePanelElementDoubleClick;

        _currentActionStateConditionsPanel.OnElementDestroyButtonClick += OnCurrentActionStateConditionsPanelElementDestroyButtonClick;

        _buttonAddActionState.onClick.AddListener(() =>
        {
            var popUpFactory = this.GetController<PopUpFactory>();
            var popUp = popUpFactory.Create<PopUpCreateActionState>();
            popUp.Show(new PopUpCreateActionStateContextData(contextData.ActionData));
            popUp.OnHide += () => _currentActionStatesPanel.Show(contextData.ActionData.States);
        });

        _buttonAddActionCondition.onClick.AddListener(() =>
        {
            if (_selectedActionState == null)
            {
                _currentActionStateConditionsPanel.Clear();
                return;
            }

            var selectedActionCellData = Table.Instance.GetTableCellData(_selectedActionState);
            var selectedActionTimeData = selectedActionCellData.GetContent<TimeData>();
            if (selectedActionTimeData.DayPart == EDayPart.Day && selectedActionTimeData.DayNumber == 1)
                return;

            var popUpFactory = this.GetController<PopUpFactory>();
            var popUp = popUpFactory.Create<PopUpCreateActionCondition>();
            popUp.Show(new PopUpCreateActionConditionContextData(_selectedActionState));
            popUp.OnHide += () =>
            {
                var states = new List<ActionState>();
                _selectedActionState.Conditions.ForEach((condition) => states.Add(condition.State));
                _currentActionStateConditionsPanel.Show(states);
            };
        });

        _buttonClose.onClick.AddListener(Hide);
    }

    protected override void InternalHide()
    {
        Clear();
    }


    private void Clear()
    {
        _selectedActionState = null;

        _textActionTimeAndActor.text = string.Empty;

        _buttonClose.onClick.RemoveAllListeners();
        _buttonAddActionState.onClick.RemoveAllListeners();
        _buttonAddActionCondition.onClick.RemoveAllListeners();

        _currentActionStatesPanel.OnElementClicked -= OnCurrentActionStatePanelElementClicked;
        _currentActionStatesPanel.OnElementDestroyButtonClick -= OnCurrentActionStatePanelElementDestroyButtonClick;

        _currentActionStateConditionsPanel.OnElementDestroyButtonClick -= OnCurrentActionStateConditionsPanelElementDestroyButtonClick;

        _currentActionStatesPanel.Clear();
        _currentActionStateConditionsPanel.Clear();
    }


    private void OnCurrentActionStatePanelElementClicked(ActionState state)
    {
        _selectedActionState = state;

        var states = new List<ActionState>();
        _selectedActionState.Conditions.ForEach((condition) => states.Add(condition.State));
        _currentActionStateConditionsPanel.Show(states);
    }

    private void OnCurrentActionStatePanelElementDestroyButtonClick(ActionState state)
    {
        if (_selectedActionState == state)
        {
            _selectedActionState = null;
            _currentActionStateConditionsPanel.Clear();
        }

        Table.Instance.RemoveStateInActionData(state);

        _currentActionStatesPanel.Show(_contextData.ActionData.States);
    }

    private void OnCurrentActionStatePanelElementDoubleClick(ActionState state)
    {
        var popUpFactory = this.GetController<PopUpFactory>();
        var popUp = popUpFactory.Create<PopUpRenameActionState>();
        popUp.Show(new PopUpRenameActionStateContextData(state));
        popUp.OnHide += () => _currentActionStatesPanel.Show(_contextData.ActionData.States);
    }


    private void OnCurrentActionStateConditionsPanelElementDestroyButtonClick(ActionState state)
    {
        var removedCondition = _selectedActionState.Conditions.Find((condition) => condition.State == state);
        _selectedActionState.Conditions.Remove(removedCondition);

        var states = new List<ActionState>();
        _selectedActionState.Conditions.ForEach((condition) => states.Add(condition.State));
        _currentActionStateConditionsPanel.Show(states);
    }
}