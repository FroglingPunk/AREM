using UnityEngine;
using UnityEngine.UI;

public class PopUpRenameActor : PopUpViewBase<PopUpRenameActorContextData>
{
    [SerializeField] private InputField _inputField;

    [SerializeField] private Button _buttonDeny;
    [SerializeField] private Button _buttonApply;


    protected override void InternalShow(PopUpRenameActorContextData contextData)
    {
        Clear();

        _inputField.text = contextData.ActorData.Name;

        _buttonApply.onClick.AddListener(() =>
        {
            if (Table.Instance.TryRenameActor(contextData.ActorData, _inputField.text))
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