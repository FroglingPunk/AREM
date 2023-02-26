using System;
using UnityEngine;

[Serializable]
public class PopUpEnterLocationContextData : PopUpContextDataBase
{
    public string Description;

    public Sprite Sprite;

    public Action CallbackButtonComeInClick;
    public Action CallbackButtonLeaveClick;


    public PopUpEnterLocationContextData(string description, Sprite sprite, Action callbackButtonComeInClick, Action callbackButtonLeaveClick)
    {
        Description = description;
        Sprite = sprite;
        CallbackButtonComeInClick = callbackButtonComeInClick;
        CallbackButtonLeaveClick = callbackButtonLeaveClick;
    }
}