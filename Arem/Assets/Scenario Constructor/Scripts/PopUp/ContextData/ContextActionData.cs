using System;

public class ContextActionData
{
    public string Name;
    public Action Callback;


    public ContextActionData(string name, Action callback)
    {
        Name = name;
        Callback = callback;
    }
}