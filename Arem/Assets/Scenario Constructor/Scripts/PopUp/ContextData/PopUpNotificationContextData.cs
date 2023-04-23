using System;

[Serializable]
public class PopUpNotificationContextData : PopUpContextDataBase
{
    public string Description;


    public PopUpNotificationContextData(string description)
    {
        Description = description;
    }
}