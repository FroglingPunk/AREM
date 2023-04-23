using System.Collections.Generic;
using System;

[Serializable]
public class SerializableTableData
{
    public ActorData[] Actors;
    public TimeData[] Times;
    public SerializableActionState[] ActionStates;

    public int Width => Times.Length + 1;
    public int Height => Actors.Length + 1;


    public List<SerializableActionState> GetStatesInCell(int cellID)
    {
        var states = new List<SerializableActionState>();

        for (var i = 0; i < ActionStates.Length; i++)
        {
            if (ActionStates[i].CellID != cellID)
                continue;

            states.Add(ActionStates[i]);
        }

        states.Sort();
        return states;
    }
}