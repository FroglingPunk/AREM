using System.Collections.Generic;
using Unity.VisualScripting;

public class ActionData : IGeneralizable
{
    public IGeneralContainer Container { get; set; }


    public List<ActionState> States;


    public ActionData(List<ActionState> states = null)
    {
        States = states == null ? new List<ActionState>() : states;
    }


    public bool TryAddState(string stateDescription)
    {
        if (string.IsNullOrEmpty(stateDescription))
            return false;

        if (States.Find((state) => state.Description == stateDescription) != null)
            return false;

        States.Add(new ActionState { Description = stateDescription });
        return true;
    }

    public bool TryRenameState(ActionState renamingState, string newStateDescription)
    {
        if (string.IsNullOrEmpty(newStateDescription))
            return false;

        if (States.Find((state) => renamingState != state && state.Description == newStateDescription) != null)
            return false;

        renamingState.Description = newStateDescription;
        return true;
    }

    public override string ToString()
    {
        return "Empty Action";
    }
}