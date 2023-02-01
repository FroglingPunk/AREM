using System;

[Serializable]
public class EntityData
{
    public EEntityType Type;
    public int Level;
    public ETeam Team;
    public FieldCellIndex FieldPosition;

    public GameParamsEntityData GameParams => this.GetController<GameParamsEntityDataStorage>().GetGameParamsEntityData(Type);
    public EntityStats Stats
    {
        get
        {
            var stats = GameParams.GetStats(Level);

            return stats;
        }
    }
}