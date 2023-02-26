using System;
using System.Collections.Generic;

public class MessageBus : ControllerBase
{
    private Dictionary<Type, Action<IMessage>> _callbacks = new Dictionary<Type, Action<IMessage>>();


    public void Callback(IMessage message)
    {
        if (!_callbacks.ContainsKey(message.GetType()))
            return;

        _callbacks[message.GetType()]?.Invoke(message);
    }

    public void Subscribe<T>(Action<IMessage> callback) where T : IMessage
    {
        if (!_callbacks.ContainsKey(typeof(T)))
            _callbacks.Add(typeof(T), callback);
        else
            _callbacks[typeof(T)] += callback;
    }

    public void Unsubscribe<T>(Action<IMessage> callback) where T : IMessage
    {
        if (!_callbacks.ContainsKey(typeof(T)))
            return;

        _callbacks[typeof(T)] -= callback;
    }
}