using System;

[Serializable]
public class FieldCellIndex
{
    public EFieldSide Side;
    public EFieldLevel Level;
    public EFieldLinePosition Position;


    public FieldCellIndex(EFieldSide side, EFieldLevel level, EFieldLinePosition position)
    {
        Side = side;
        Level = level;
        Position = position;
    }


    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        var other = obj as FieldCellIndex;
        return Side == other.Side && Level == other.Level && Position == other.Position;
    }

    public bool Equals(EFieldSide side, EFieldLevel level, EFieldLinePosition position)
    {
        return Side == side && Level == level && Position == position;
    }

    public override string ToString()
    {
        return $"Side : {Side} |Level : {Level} |Position : {Position}";
    }
}