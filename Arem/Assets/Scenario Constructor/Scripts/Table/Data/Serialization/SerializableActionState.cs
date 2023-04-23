using System;

[Serializable]
public class SerializableActionState : IComparable<SerializableActionState>
{
    public int CellID;
    public int ID;
    public string Description;
    public SerializableActionCondition[] Conditions;

    public int CompareTo(SerializableActionState other)
    {
        return ID.CompareTo(other.ID);
    }
}