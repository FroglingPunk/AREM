using UnityEngine;

public class EdgeTableCellContextButton : EdgeTableCellBase
{
    private PopUpContextActions _popUp;


    public override void Init(TableCellData data)
    {
        base.Init(data);

        mouseHandler.OnClickCallback += OnClick;
    }

    public override void UpdateView()
    {

    }

    private void OnClick()
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
            new ContextActionData("Добавить 1/3 дня", Table.Instance.AddColumn),
            new ContextActionData("Добавить актёра", () =>
            {
                popUpFactory.Create<PopUpCreateActor>().Show(default);
            })
        }));
    }
}