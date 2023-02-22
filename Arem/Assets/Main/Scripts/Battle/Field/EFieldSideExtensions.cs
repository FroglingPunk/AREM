public static class EFieldSideExtensions
{
    public static EFieldSide Opposite(this EFieldSide side)
    {
        return side == EFieldSide.Left ? EFieldSide.Right : EFieldSide.Left;
    }
}