using System;

[Serializable]
public class PopUpAdjustActionContextData : PopUpContextDataBase
{
    public ActionData ActionData;


    public PopUpAdjustActionContextData(ActionData actionData)
    {
        ActionData = actionData;
    }
}