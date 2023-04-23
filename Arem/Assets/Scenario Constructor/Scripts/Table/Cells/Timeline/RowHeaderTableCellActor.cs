using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowHeaderTableCellActor : RowHeaderTableCellBase
{
    private PopUpContextActions _popUp;


    public override void Init(TableCellData data)
    {
        base.Init(data);

        var textComponent = GetComponentInChildren<Text>();
        textComponent.text = data.GetContent<ActorData>().Name;

        mouseHandler.OnDoubleClickCallback += OnDoubleClick;
    }


    public override void UpdateView()
    {
        var textComponent = GetComponentInChildren<Text>();
        textComponent.text = Data.GetContent<ActorData>().Name;
    }

    protected void OnDoubleClick()
    {
        if (_popUp && _popUp.gameObject.activeSelf)
        {
            _popUp.Hide();
            return;
        }

        var popUpFactory = this.GetController<PopUpFactory>();

        _popUp = popUpFactory.Create<PopUpContextActions>();

        _popUp.Show(new PopUpContextActionsContextData(Input.mousePosition, new ContextActionData[]
        {
            new ContextActionData("Переименовать", () =>
            {
                var popUpFactory = this.GetController<PopUpFactory>();
                var popUp = popUpFactory.Create<PopUpRenameActor>();
                popUp.Show(new PopUpRenameActorContextData(Data.GetContent<ActorData>()));
            }),

            new ContextActionData("Удалить", () =>
            {
                Table.Instance.RemoveRow(Data.YID);
            })
        }));
    }
}