using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "EntityData", menuName = "GameParams/Entity Data", order = 0)]
public class GameParamsEntityData : ScriptableObject
{
    [SerializeField] private AssetReference _prefab;
    [SerializeField] private EEntityType _entityType;
    [SerializeField] private LevelEntityStats[] _levelEntityStats;
    [SerializeField] private Skill[] _skills;


    public AssetReference Prefab => _prefab;
    public EEntityType EntityType => _entityType;
    public Skill[] Skills => _skills;
    public EntityStats GetStats(int level)
    {
        for (int i = 0; i < _levelEntityStats.Length; i++)
        {
            if (_levelEntityStats[i].Level == level)
            {
                return _levelEntityStats[i].Stats.Clone() as EntityStats;
            }
        }

        Debug.LogError($"Not found EntityStats for {_entityType} in {level} level");
        return null;
    }
}