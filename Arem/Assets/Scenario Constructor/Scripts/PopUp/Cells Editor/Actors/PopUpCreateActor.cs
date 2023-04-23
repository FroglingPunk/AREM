using UnityEngine;
using UnityEngine.UI;

public class PopUpCreateActor : PopUpViewBase<PopUpContextDataBase>
{
    [SerializeField] private InputField _inputField;

    [SerializeField] private Button _buttonDeny;
    [SerializeField] private Button _buttonApply;


    protected override void InternalShow(PopUpContextDataBase contextData)
    {
        Clear();

        _buttonApply.onClick.AddListener(() =>
        {
            if (Table.Instance.TryAddActorRow(_inputField.text))
                Hide();
            else
            {
                var popUpFactory = this.GetController<PopUpFactory>();
                var popUp = popUpFactory.Create<PopUpNotification>();
                popUp.Show(new PopUpNotificationContextData("Имя актёра не должно быть пустым или повторять другие имена!"));
            }
        });

        _buttonDeny.onClick.AddListener(Hide);
    }

    protected override void InternalHide()
    {
        Clear();
    }


    private void Clear()
    {
        _inputField.text = string.Empty;

        _buttonApply.onClick.RemoveAllListeners();
        _buttonDeny.onClick.RemoveAllListeners();
    }
}