using UnityEngine;
using UnityEngine.UI;

public class PopUpNotification : PopUpViewBase<PopUpNotificationContextData>
{
    [SerializeField] private Text _textDescription;

    [SerializeField] private Button _buttonClose;


    protected override void InternalShow(PopUpNotificationContextData contextData)
    {
        Clear();

        _textDescription.text = contextData.Description;
        _buttonClose.onClick.AddListener(Hide);
    }

    protected override void InternalHide()
    {
        Clear();
    }


    private void Clear()
    {
        _textDescription.text = string.Empty;
        _buttonClose.onClick.RemoveAllListeners();
    }
}