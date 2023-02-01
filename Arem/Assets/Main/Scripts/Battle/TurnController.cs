using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnController : ControllerBase
{
    public CallbackVariable<Entity> CurrentTurnEntity { get; private set; } = new CallbackVariable<Entity>();
    public CallbackVariable<int> Turn { get; private set; } = new CallbackVariable<int>();
    public List<Entity> Queue { get; private set; } = new List<Entity>();

    private uint _currentQueueID;


    public void StartFirstTurn()
    {
        if (Turn.Value > 0)
        {
            Debug.LogError("Trying to start first turn on running turn controller");
            return;
        }

        SetTurn(1);
    }

    private void NextTurn()
    {
        SetTurn(Turn.Value + 1);
    }

    private void SetTurn(int turn)
    {
        RecalculateQueue();
        Turn.Value = turn;

        SetQueue(0);
    }

    public void NextQueue()
    {
        if(_currentQueueID == Queue.Count - 1)
        {
            NextTurn();
        }
        else
        {
            SetQueue(_currentQueueID + 1);
        }
    }

    private void SetQueue(uint queueID)
    {
        if (queueID >= Queue.Count)
        {
            Debug.LogError("Trying to set queue id out of range");
            return;
        }

        _currentQueueID = queueID;
        CurrentTurnEntity.Value = Queue[(int)_currentQueueID];
    }

    private void RecalculateQueue()
    {
        Queue.Clear();

        var entities = this.GetController<EntitiesManager>().Entities.ToList();

        entities.Sort((a, b) =>
        {
            if (a.Stats.Speed > b.Stats.Speed)
            {
                return 1;
            }
            else if (a.Stats.Speed < b.Stats.Speed)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });

        Queue = entities;
    }
}

public class EntityActionPointsController
{
    public void Init()
    {
        //var skillExecutionController = this.GetController<SkillExecitionController>();
        //skillExecutionController.OnSkillFinishExecuting += OnSkillExecuted;
    }


    private void OnSkillExecuted(Skill skill)
    {
        var turnController = this.GetController<TurnController>();
        //turnController.CurrentTurnEntity.Value.
    }
}