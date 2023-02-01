using System;
using System.Collections;
using UnityEngine;

public class CustomActionStep : IActionStep
{
    private Action _action;
    private float _delay;


    public CustomActionStep(Action action, float delay = 0f)
    {
        _action = action;
        _delay = delay;
    }


    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(_delay);
        _action?.Invoke();
        yield return null;
    }
}