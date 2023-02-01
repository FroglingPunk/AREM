using System.Collections;

public class StartBattleActionStep : IActionStep
{
    public StartBattleActionStep() { }


    public IEnumerator Execute()
    {
        this.GetController<BattleManager>().StartBattle();

        yield return null;
    }
}