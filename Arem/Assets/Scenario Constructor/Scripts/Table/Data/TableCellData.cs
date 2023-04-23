using System;
using System.Collections.Generic;

public class TableCellData : IGeneralContainer
{
    public Dictionary<Type, IGeneralizable> Content { get; set; }

    public int XID;
    public int YID;

    public T GetContent<T>() where T : IGeneralizable
    {
        if (Content.TryGetValue(typeof(T), out var value))
            return (T)value;

        return default;
    }


    public TableCellData(int xID, int yID, params IGeneralizable[] content)
    {
        XID = xID;
        YID = yID;

        Content = new Dictionary<Type, IGeneralizable>();

        for (var i = 0; i < content.Length; i++)
        {
            var contentElement = content[i];
            contentElement.Container = this;
            Content.Add(contentElement.GetType(), contentElement);
        }
    }


    public override string ToString()
    {
        var text = string.Empty;

        foreach (var element in Content.Values)
        {
            text += element.ToString();
            text += "\n";
        }

        return text;
    }
}