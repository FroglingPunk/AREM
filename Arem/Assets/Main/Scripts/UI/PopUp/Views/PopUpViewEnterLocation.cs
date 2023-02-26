using UnityEngine;
using UnityEngine.UI;

public class PopUpViewEnterLocation : PopUpViewBase<PopUpEnterLocationContextData>
{
    [SerializeField] private Image _imageLocation;

    [SerializeField] private Text _textDescription;

    [SerializeField] private Button _buttonComeIn;
    [SerializeField] private Button _buttonLeave;


    protected override void InternalShow(PopUpEnterLocationContextData contextData)
    {
        _imageLocation.sprite = contextData.Sprite;
        _textDescription.text = contextData.Description;

        _buttonComeIn.onClick.AddListener(() => contextData.CallbackButtonComeInClick?.Invoke());
        _buttonLeave.onClick.AddListener(() => contextData.CallbackButtonLeaveClick?.Invoke());
    }

    protected override void InternalHide()
    {
        _buttonComeIn.onClick.RemoveAllListeners();
        _buttonLeave.onClick.RemoveAllListeners();
    }
}