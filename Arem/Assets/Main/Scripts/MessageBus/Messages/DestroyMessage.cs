public class DestroyMessage<T> : IMessage
{
    public readonly T Destroyed;


    public DestroyMessage(T destroyed)
    {
        Destroyed = destroyed;
    }
}