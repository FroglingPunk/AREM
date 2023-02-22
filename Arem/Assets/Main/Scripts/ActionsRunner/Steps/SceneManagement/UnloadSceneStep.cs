using System.Collections;
using UnityEngine.AddressableAssets;

public class UnloadSceneStep : SceneManagementStepActionBase
{
    public readonly string SceneName;


    public UnloadSceneStep(string sceneName) : base()
    {
        SceneName = sceneName;
    }


    public override IEnumerator Execute()
    {
        var scene = _activeScenesPool.Pull(SceneName);
        var handle = Addressables.UnloadSceneAsync(scene);
        yield return handle;
    }
}