using System.Collections;

public class InitEntityDataStorageActionStep : IActionStep
{
    public InitEntityDataStorageActionStep() { }


    public IEnumerator Execute()
    {
        var controller = ControllersContainer.GetController<GameParamsEntityDataStorage>();

        if (controller == null)
            controller = ControllersContainer.CreateController<GameParamsEntityDataStorage>();

        yield return controller.InitStorage();
    }
}