public abstract class GameControllerBase
{
    public EGameControllerState State { get; protected set; } = EGameControllerState.Inactive;
    public abstract ETeam ControlledTeam { get; }


    public abstract void TransferControl(bool isActive);
}