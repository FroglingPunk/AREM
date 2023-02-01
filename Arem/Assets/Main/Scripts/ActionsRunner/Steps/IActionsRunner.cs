public interface IActionsRunner
{
    public void Run();
    public void Setup(params IActionStep[] steps);
}