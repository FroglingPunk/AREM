using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PopUpFactory : MonoBehaviour
{
    private static Dictionary<Type, PopUpViewBase> _prefabs = new Dictionary<Type, PopUpViewBase>();

    private Dictionary<Type, PopUpViewBase> _popUps = new Dictionary<Type, PopUpViewBase>();
    private Canvas _canvas;


    private void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
    }


    public T Create<T>() where T : PopUpViewBase
    {
        if (_popUps.ContainsKey(typeof(T)))
            return _popUps[typeof(T)] as T;

        var popUp = Instantiate(_prefabs[typeof(T)], _canvas.transform);
        _popUps.Add(typeof(T), popUp);

        popUp.Hide();

        return popUp as T;
    }

    public void HideAllPopUps()
    {
        foreach (var popUp in _popUps.Values)
            popUp.Hide();
    }

    public static IEnumerator LoadPopUpPrefabs()
    {
        if (_prefabs.Count > 0)
            yield break;

        var handle = Addressables.LoadAssetsAsync<GameObject>("PopUp", (popUp) =>
        {
            var component = popUp.GetComponent<PopUpViewBase>();
            _prefabs.Add(component.GetType(), component);
        });

        yield return handle;
    }
}