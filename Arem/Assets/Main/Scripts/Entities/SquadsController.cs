using System.Collections.Generic;
using Unity.VisualScripting;

public class SquadsController : ControllerBase
{
    private List<EntityData> _playerSquad;
    private List<EntityData> _enemySquad;


    public ICollection<EntityData> GetSquadData(ETeam team)
    {
        switch (team)
        {
            case ETeam.Player:
                return _playerSquad.AsReadOnlyCollection();

            case ETeam.EnemyAI:
                return _enemySquad.AsReadOnlyCollection();

            default:
                return default;
        }
    }


    protected override void InternalInit()
    {
        base.InternalInit();

        _playerSquad = new List<EntityData>();
        _playerSquad.Add(new EntityData { Team = ETeam.Player, Type = EEntityType.HeroKnight, Level = 2, FieldPosition = new FieldCellIndex(EFieldSide.Left, EFieldLevel.Ground, EFieldLinePosition.First) });
        _playerSquad.Add(new EntityData { Team = ETeam.Player, Type = EEntityType.HeroArcher, Level = 3, FieldPosition = new FieldCellIndex(EFieldSide.Left, EFieldLevel.Ground, EFieldLinePosition.Second) });

        _enemySquad = new List<EntityData>();
        _enemySquad.Add(new EntityData { Team = ETeam.EnemyAI, Type = EEntityType.HeroKnight, Level = 2, FieldPosition = new FieldCellIndex(EFieldSide.Right, EFieldLevel.Ground, EFieldLinePosition.First) });
        _enemySquad.Add(new EntityData { Team = ETeam.EnemyAI, Type = EEntityType.HeroArcher, Level = 3, FieldPosition = new FieldCellIndex(EFieldSide.Right, EFieldLevel.Ground, EFieldLinePosition.Second) });
    }
}