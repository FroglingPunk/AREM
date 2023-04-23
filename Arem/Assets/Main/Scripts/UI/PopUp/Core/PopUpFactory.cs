using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PopUpFactory : MonoControllerBase
{
    private Dictionary<Type, GameObject> _prefabs = new Dictionary<Type, GameObject>();
    private Dictionary<Type, IPopUpView> _popUps = new Dictionary<Type, IPopUpView>();

    private Canvas _canvas;


    protected override void InternalInit()
    {
        base.InternalInit();

        if (ControllersContainer.ContainsController<PopUpFactory>())
        {
            Destroy(gameObject);
            return;
        }

        ControllersContainer.AddController(this);
        DontDestroyOnLoad(gameObject);

        _canvas = GetComponentInChildren<Canvas>();
        StartCoroutine(LoadPopUpPrefabs());
    }

    protected override void InternalDestroy()
    {
       
    }

    public T Create<T>() where T : IPopUpView
    {
        if (_popUps.ContainsKey(typeof(T)))
            return (T)_popUps[typeof(T)];

        var popUp = Instantiate(_prefabs[typeof(T)], _canvas.transform).GetComponent<IPopUpView>();
        _popUps.Add(typeof(T), popUp);

        popUp.Hide();

        return (T)popUp;
    }

    public void HideAllPopUps()
    {
        foreach (var popUp in _popUps.Values)
            popUp.Hide();
    }


    private IEnumerator LoadPopUpPrefabs()
    {
        if (_prefabs.Count > 0)
            yield break;

        var handle = Addressables.LoadAssetsAsync<GameObject>("PopUp", (popUp) =>
        {
            var component = popUp.GetComponent<IPopUpView>();
            _prefabs.Add(component.GetType(), popUp);
        });

        yield return handle;
    }
}