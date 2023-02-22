using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public class ActiveScenesPool
{
    private static Dictionary<string, SceneInstance> _additiveLoadedScenes = new Dictionary<string, SceneInstance>();


    public void Push(string sceneName, SceneInstance sceneInstance)
    {
        if (_additiveLoadedScenes.ContainsKey(sceneName))
        {
            Debug.LogError($"Scenes pool has already {sceneName} scene");
            _additiveLoadedScenes[sceneName] = sceneInstance;
        }
        else
            _additiveLoadedScenes.Add(sceneName, sceneInstance);
    }

    public SceneInstance Pull(string sceneName)
    {
        if (_additiveLoadedScenes.ContainsKey(sceneName))
        {
            var scene = _additiveLoadedScenes[sceneName];
            _additiveLoadedScenes.Remove(sceneName);
            return scene;
        }

        Debug.LogError($"Scene pool does not contain {sceneName} scene");
        return default;
    }
}