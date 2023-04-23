using System.Collections.Generic;

public class ActionState
{
    public string Description;
    public bool IsActive;
    public List<ActionCondition> Conditions = new List<ActionCondition>();


    public bool TryAddCondition(ActionState state, bool isActive)
    {
        if (Conditions.Find((condition) => condition.State == state) != null)
            return false;

        var stateCellData = Table.Instance.GetTableCellData(state);
        var otherStatesInCell = stateCellData.GetContent<ActionData>().States;

        for(var i = 0; i < Conditions.Count; i++)
        {
            if (otherStatesInCell.Contains(Conditions[i].State))
                return false;
        }

        Conditions.Add(new ActionCondition { State = state, IsActive = isActive });
        return true;
    }
}