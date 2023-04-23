using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopUpCreateActionCondition : PopUpViewBase<PopUpCreateActionConditionContextData>
{
    [SerializeField] private Button _buttonDeny;

    [SerializeField] private Dropdown _dropdownTime;
    [SerializeField] private Dropdown _dropdownActors;

    [SerializeField] private ActionStatesPanel _actionStatesPanel;

    private Dictionary<string, TimeData> _timesDataDictionary = new Dictionary<string, TimeData>();
    private Dictionary<string, ActorData> _actorsDataDictionary = new Dictionary<string, ActorData>();

    private ActionState _currentState;


    protected override void InternalShow(PopUpCreateActionConditionContextData contextData)
    {
        Clear();

        _currentState = contextData.ActionState;

        _actionStatesPanel.OnElementClicked += OnActionStateClicked;

        var cellData = Table.Instance.GetTableCellData(contextData.ActionState);
        var actionTimeData = cellData.GetContent<TimeData>();
        var allTimeData = Table.Instance.GetAllTimesData();
        allTimeData.ForEach((data) =>
        {
            if (data.DayNumber < actionTimeData.DayNumber || (data.DayNumber == actionTimeData.DayNumber && data.DayPart < actionTimeData.DayPart))
                _timesDataDictionary.Add(data.ToString(), data);
        });
        var timeOptions = new List<Dropdown.OptionData>();
        foreach (var timeText in _timesDataDictionary.Keys)
            timeOptions.Add(new Dropdown.OptionData(timeText));
        _dropdownTime.options = timeOptions;
        _dropdownTime.onValueChanged.AddListener((value) => UpdateStatesPanel());

        var allActorsData = Table.Instance.GetAllActorsData();
        allActorsData.ForEach((data) => _actorsDataDictionary.Add(data.ToString(), data));
        var actorsOptions = new List<Dropdown.OptionData>();
        foreach (var actorText in _actorsDataDictionary.Keys)
            actorsOptions.Add(new Dropdown.OptionData(actorText));
        _dropdownActors.options = actorsOptions;
        _dropdownActors.onValueChanged.AddListener((value) => UpdateStatesPanel());

        _buttonDeny.onClick.AddListener(Hide);

        UpdateStatesPanel();
    }

    protected override void InternalHide()
    {
        Clear();
    }


    private void UpdateStatesPanel()
    {
        var selectedTimeData = _timesDataDictionary[_dropdownTime.options[_dropdownTime.value].text];
        var selectedActorData = _actorsDataDictionary[_dropdownActors.options[_dropdownActors.value].text];

        _actionStatesPanel.Show(Table.Instance.GetActionData(selectedTimeData, selectedActorData).States);
    }

    private void Clear()
    {
        _currentState = null;

        _actionStatesPanel.OnElementClicked -= OnActionStateClicked;

        _timesDataDictionary.Clear();
        _actorsDataDictionary.Clear();

        _buttonDeny.onClick.RemoveAllListeners();
        _dropdownActors.onValueChanged.RemoveAllListeners();
        _dropdownTime.onValueChanged.RemoveAllListeners();

        _actionStatesPanel.Clear();
    }

    private void OnActionStateClicked(ActionState actionState)
    {
        if (_currentState.TryAddCondition(actionState, true))
            Hide();
        else
        {
            var popUpFactory = this.GetController<PopUpFactory>();
            var popUp = popUpFactory.Create<PopUpNotification>();
            popUp.Show(new PopUpNotificationContextData("Недопустимая связь!"));
        }
    }
}