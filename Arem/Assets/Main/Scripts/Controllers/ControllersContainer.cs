using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ControllersContainer
{
    private static Dictionary<Type, IController> _controllers = new Dictionary<Type, IController>();


    public static void AddController<T>(T controller) where T : IController
    {
        if (_controllers.ContainsKey(typeof(T)))
        {
            Debug.LogError($"Controllers container already contains {typeof(T)} controller");
            return;
        }

        _controllers.Add(typeof(T), controller);
    }

    public static void AddControllerAs<T, U>(T controller) where T : IController
    {
        if (_controllers.ContainsKey(typeof(U)))
        {
            Debug.LogError($"Controllers container already contains {typeof(T)} controller as {typeof(U)}");
            return;
        }

        _controllers.Add(typeof(U), controller);
    }

    public static T CreateMonoController<T>() where T : MonoControllerBase
    {
        if (_controllers.ContainsKey(typeof(T)))
            return _controllers[typeof(T)] as T;

        T controller = new GameObject(typeof(T).Name).AddComponent<T>();
        _controllers.Add(typeof(T), controller);
        return controller;
    }

    public static T CreateMonoControllerAs<T, U>() where T : MonoControllerBase where U : IController 
    {
        if (_controllers.ContainsKey(typeof(U)))
            return _controllers[typeof(U)] as T;

        T controller = new GameObject(typeof(T).Name).AddComponent<T>();
        _controllers.Add(typeof(U), controller);
        return controller;
    }

    public static T CreateController<T>() where T : ControllerBase, new()
    {
        if (_controllers.ContainsKey(typeof(T)))
            return _controllers[typeof(T)] as T;

        var controller = new T();
        _controllers.Add(typeof(T), controller);
        return controller;
    }

    public static T CreateControllerAs<T, U>() where T : ControllerBase, new()
    {
        if (_controllers.ContainsKey(typeof(U)))
            return _controllers[typeof(U)] as T;

        T controller = new T();
        _controllers.Add(typeof(U), controller);
        return controller;
    }

    public static void RemoveController(IController controller)
    {
        if (_controllers.Values.Contains(controller))
        {
            var key = _controllers.FirstOrDefault(x => x.Value == controller).Key;
            _controllers.Remove(key);
        }
        else
        {
            _controllers.Remove(controller.GetType());
        }
    }

    public static T GetController<T>() where T : class, IController 
    {
        return _controllers.ContainsKey(typeof(T)) ? (T)_controllers[typeof(T)] : null;
    }

    public static bool ContainsController<T>()
    {
        return _controllers.ContainsKey(typeof(T));
    }
}