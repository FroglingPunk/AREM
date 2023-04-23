using System;

[Serializable]
public class PopUpCreateActionStateContextData : PopUpContextDataBase
{
    public readonly ActionData ActionData;


    public PopUpCreateActionStateContextData(ActionData actionData)
    {
        ActionData = actionData;
    }
}