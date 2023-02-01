using System.Collections.Generic;
using System.Collections.ObjectModel;

public class EntitiesManager : ControllerBase
{
    private List<Entity> _entities = new List<Entity>(16);

    public ReadOnlyCollection<Entity> Entities => _entities.AsReadOnly();
    public List<Entity> this[ETeam team] => _entities.FindAll((entity) => entity.Team == team);


    protected override void InternalInit()
    {
        base.InternalInit();

        var messageBus = this.GetController<MessageBus>();

        messageBus.Subscribe<CreateMessage<Entity>>((msg) => _entities.Add((msg as CreateMessage<Entity>).Created));
        messageBus.Subscribe<DestroyMessage<Entity>>((msg) => _entities.Remove((msg as DestroyMessage<Entity>).Destroyed));
    }
}