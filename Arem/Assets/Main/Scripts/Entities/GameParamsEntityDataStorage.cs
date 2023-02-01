using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

public class GameParamsEntityDataStorage : ControllerBase
{
    private List<GameParamsEntityData> _gameParamsEntitiesData = new List<GameParamsEntityData>();


    public GameParamsEntityData GetGameParamsEntityData(EEntityType entityType)
    {
        return _gameParamsEntitiesData.Find((x) => x.EntityType == entityType);
    }


    protected override void InternalInit()
    {
        base.InternalInit();
    }


    public IEnumerator InitStorage()
    {
        var handle = Addressables.LoadAssetsAsync<GameParamsEntityData>("GameParamsEntityData", _gameParamsEntitiesData.Add);
        yield return handle;
    }
}