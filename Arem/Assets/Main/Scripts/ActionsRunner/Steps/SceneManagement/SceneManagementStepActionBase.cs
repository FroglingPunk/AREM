using System.Collections;

public abstract class SceneManagementStepActionBase : IActionStep
{
    protected static ActiveScenesPool _activeScenesPool;

    public SceneManagementStepActionBase()
    {
        if (_activeScenesPool == null)
            _activeScenesPool = new ActiveScenesPool();
    }

    public abstract IEnumerator Execute();
}