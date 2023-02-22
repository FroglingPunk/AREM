using UnityEngine.SceneManagement;

public class TestPlayerTriggerEnterCrusade : PlayerTriggerBase
{
    protected override void OnPlayerEnter(TestPlayer player)
    {
        var runner = new ActionsRunner();
        runner.Setup(new LoadSceneStep("Crusade", LoadSceneMode.Single));
        runner.Run();
    }

    protected override void OnPlayerExit(TestPlayer player)
    {

    }
}