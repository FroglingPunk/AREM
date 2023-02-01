using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class ActionsRunner : IActionsRunner
{
    public bool IsRun { get; private set; } = false;

    private Queue<IActionStep> _actions = new Queue<IActionStep>();


    public void Run()
    {
        if (IsRun)
            return;

        CoroutinesRunner.Instance.StartCoroutine(RunCoroutine());
    }

    public virtual void Setup(params IActionStep[] actionSteps)
    {
        for (int i = 0; i < actionSteps.Length; i++)
            _actions.Enqueue(actionSteps[i]);
    }


    private IEnumerator RunCoroutine()
    {
        IsRun = true;

        while (_actions.Count > 0)
            yield return _actions.Dequeue().Execute();

        IsRun = false;
    }
}