using UnityEngine;
using UnityEngine.UI;

public class PopUpCreateActionState : PopUpViewBase<PopUpCreateActionStateContextData>
{
    [SerializeField] private InputField _inputField;

    [SerializeField] private Button _buttonDeny;
    [SerializeField] private Button _buttonApply;


    protected override void InternalShow(PopUpCreateActionStateContextData contextData)
    {
        Clear();

        _buttonApply.onClick.AddListener(() =>
        {
            if (_contextData.ActionData.TryAddState(_inputField.text))
                Hide();
            else
            {
                var popUpFactory = this.GetController<PopUpFactory>();
                var popUp = popUpFactory.Create<PopUpNotification>();
                popUp.Show(new PopUpNotificationContextData("Описание состояния не должно быть пустым или повторять другие состояния!"));
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