using System.Collections;
using UnityEngine.AddressableAssets;

public class InstantiateEntitiesActionStep : IActionStep
{
    private ETeam _team;


    public InstantiateEntitiesActionStep(ETeam team)
    {
        _team = team;
    }


    public IEnumerator Execute()
    {
        var squadsController = this.GetController<SquadsController>();
        var entityDataStorage = this.GetController<GameParamsEntityDataStorage>();

        var squad = squadsController.GetSquadData(_team);

        foreach (var entity in squad)
        {
            var gameParamsEntityData = entityDataStorage.GetGameParamsEntityData(entity.Type);

            yield return InstantiateEntity(gameParamsEntityData.Prefab, entity);
        }
    }


    private IEnumerator InstantiateEntity(AssetReference prefab, EntityData entityData)
    {
        var handle = Addressables.InstantiateAsync(prefab);

        while (!handle.IsDone)
        {
            yield return null;
        }

        handle.Result.GetComponent<Entity>().Init(entityData);
    }
}