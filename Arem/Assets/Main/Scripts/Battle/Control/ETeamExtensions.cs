public static class ETeamExtensions
{
    public static ETeam OppositeTeam(this ETeam team)
    {
        return team == ETeam.Player ? ETeam.EnemyAI : ETeam.Player;
    }
}