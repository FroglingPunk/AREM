using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnHeaderTableCellTime : ColumnHeaderTableCellBase
{
    private PopUpContextActions _popUp;


    public override void Init(TableCellData data)
    {
        base.Init(data);

        var textComponent = GetComponentInChildren<Text>();
        textComponent.text = data.GetContent<TimeData>().ToString();

        mouseHandler.OnDoubleClickCallback += OnDoubleClick;
    }

    public override void UpdateView()
    {
        var textComponent = GetComponentInChildren<Text>();
        textComponent.text = Data.GetContent<TimeData>().ToString();
    }


    protected void OnDoubleClick()
    {
        if (_popUp && _popUp.gameObject.activeSelf)
        {
            _popUp.Hide();
            return;
        }

        if (Data.XID != Table.Instance.Width - 1)
            return;

        var popUpFactory = this.GetController<PopUpFactory>();

        _popUp = popUpFactory.Create<PopUpContextActions>();

        _popUp.Show(new PopUpContextActionsContextData(Input.mousePosition, new ContextActionData[]
        {
            new ContextActionData("Удалить", () =>
            {
                Table.Instance.RemoveLastColumn();
            })
        }));
    }
}