using System;

[Serializable]
public class PopUpRenameActionStateContextData : PopUpContextDataBase
{
    public readonly ActionState ActionState;


    public PopUpRenameActionStateContextData(ActionState actionState)
    {
        ActionState = actionState;
    }
}