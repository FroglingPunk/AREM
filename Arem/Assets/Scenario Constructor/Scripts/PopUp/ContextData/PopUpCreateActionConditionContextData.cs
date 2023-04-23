using System;

[Serializable]
public class PopUpCreateActionConditionContextData : PopUpContextDataBase
{
    public readonly ActionState ActionState;


    public PopUpCreateActionConditionContextData(ActionState actionState)
    {
        ActionState = actionState;
    }
}