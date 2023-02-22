using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LoadSceneStep : SceneManagementStepActionBase
{
    public readonly string SceneName;
    public readonly LoadSceneMode LoadMode;


    public LoadSceneStep(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single) : base()
    {
        SceneName = sceneName;
        LoadMode = loadSceneMode;
    }


    public override IEnumerator Execute()
    {
        var handle = Addressables.LoadSceneAsync(SceneName, LoadMode);
        yield return handle;

        if (LoadMode == LoadSceneMode.Additive)
            _activeScenesPool.Push(SceneName, handle.Result);
    }
}