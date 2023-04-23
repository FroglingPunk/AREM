using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCellAction : TableCellBase
{
    [SerializeField] private ActionStatesPanel _actionStatesPanel;


    public override void Init(TableCellData data)
    {
        base.Init(data);

        mouseHandler.OnDoubleClickCallback += OnDoubleClick;

        _actionStatesPanel.Show(data.GetContent<ActionData>().States);
        _actionStatesPanel.OnElementMouseEnter += OnElementMouseEnter;
        _actionStatesPanel.OnElementMouseExit += OnElementMouseExit;
    }

    public override void UpdateView()
    {
        _actionStatesPanel.Show(Data.GetContent<ActionData>().States);
    }

    private void OnDoubleClick()
    {
        var popUpFactory = this.GetController<PopUpFactory>();
        var popUp = popUpFactory.Create<PopUpAdjustAction>();
        popUp.Show(new PopUpAdjustActionContextData(Data.GetContent<ActionData>()));
        popUp.OnHide += Table.Instance.UpdateCells;
    }



    private void OnElementMouseEnter(ActionState actionState)
    {
        _actionStatesPanel.GetActionStateView(actionState).SetColor(Color.blue);

        actionState.Conditions.ForEach((condition) =>
        {
            var cell = Table.Instance.GetTableCellAction(condition.State);
            cell._actionStatesPanel.GetActionStateView(condition.State).SetColor(Color.green);
        });
    }

    private void OnElementMouseExit(ActionState actionState)
    {
        _actionStatesPanel.GetActionStateView(actionState).SetDefaultColor();

        actionState.Conditions.ForEach((condition) =>
        {
            var cell = Table.Instance.GetTableCellAction(condition.State);
            cell._actionStatesPanel.GetActionStateView(condition.State).SetDefaultColor();
        });
    }
}