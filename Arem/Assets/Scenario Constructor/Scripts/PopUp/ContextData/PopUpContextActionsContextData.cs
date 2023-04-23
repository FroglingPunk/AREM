using System;
using UnityEngine;

[Serializable]
public class PopUpContextActionsContextData : PopUpContextDataBase
{
    public readonly Vector3 Position;
    public readonly ContextActionData[] ActionsData;


    public PopUpContextActionsContextData(Vector3 position, ContextActionData[] actionsData)
    {
        Position = position;
        ActionsData = actionsData;
    }
}