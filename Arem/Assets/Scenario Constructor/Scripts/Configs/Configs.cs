using UnityEngine.AddressableAssets;

public class Configs : ControllerBase
{
    public TableConfiguration TableConfiguration { get; private set; }


    protected override void InternalInit()
    {
        base.InternalInit();

        var handle = Addressables.LoadAssetAsync<TableConfiguration>("Table Configuration");
        handle.Completed += (configHandle) => TableConfiguration = configHandle.Result;
    }
}