public class CreateMessage<T> : IMessage
{
    public readonly T Created;


    public CreateMessage(T created)
    {
        Created = created;
    }
}