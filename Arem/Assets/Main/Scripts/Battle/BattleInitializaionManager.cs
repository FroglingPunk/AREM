using UnityEngine;

public class BattleInitializaionManager : MonoBehaviour
{
    [SerializeField] private GameObject _testAllConflictElements;


    private void Start()
    {
        Init();
    }


    public void Init()
    {
        var loadingScreen = GetComponentInChildren<LoadingScreen>(true);
        loadingScreen.Show();

        var runner = new ActionsRunner();
        runner.Setup(new InitEntityDataStorageActionStep());

        runner.Setup(CreateControllers());

        runner.Setup(
            new CustomActionStep(() => _testAllConflictElements.SetActive(true)),
            new InstantiateEntitiesActionStep(ETeam.Player),
            new InstantiateEntitiesActionStep(ETeam.EnemyAI),
            new StartBattleActionStep(),
            new CustomActionStep(() => loadingScreen.Hide())
            );

        runner.Run();
    }


    private CustomActionStep CreateControllers()
    {
        return new CustomActionStep(() =>
        {
            ControllersContainer.CreateController<MessageBus>().Init();
            ControllersContainer.CreateController<EntitiesManager>().Init();
            ControllersContainer.CreateController<SquadsController>().Init();
            ControllersContainer.CreateController<TurnController>().Init();
            ControllersContainer.CreateController<SkillsManager>().Init();
            ControllersContainer.CreateController<BattleManager>().Init();
            ControllersContainer.CreateMonoController<AudioPlayer>();
        });
    }
}