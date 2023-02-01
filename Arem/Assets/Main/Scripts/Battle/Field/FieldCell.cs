using UnityEngine;

[RequireComponent(typeof(FieldCellHighlighter))]
public class FieldCell : MonoBehaviour, IHighlightable
{
    public FieldCellIndex Index;

    [HideInInspector] public Entity EntityOnPosition;

    public HighlighterBase Highlighter => GetComponent<HighlighterBase>();
    public ECellMarkState MarkState { get; private set; }


    public void Mark(ECellMarkState state)
    {
        MarkState = state;

        if (state == ECellMarkState.Inactive)
            Highlighter.DisableHighlight();
        else
            Highlighter.EnableHighlight();
    }


    #region auto setting variables in inspector
    private void Reset()
    {
        var parentName = transform.parent.name.ToLower();

        var side = parentName.Contains("left") ? EFieldSide.Left : EFieldSide.Right;
        var level = EFieldLevel.Ground;

        if (parentName.Contains("under"))
        {
            level = EFieldLevel.Under;
        }
        else if (parentName.Contains("above"))
        {
            level = EFieldLevel.Above;
        }

        var position = EFieldLinePosition.First;
        var lowerName = name.ToLower();

        if (lowerName.Contains("1"))
        {
            position = EFieldLinePosition.First;
        }
        else if (lowerName.Contains("2"))
        {
            position = EFieldLinePosition.Second;
        }
        else if (lowerName.Contains("3"))
        {
            position = EFieldLinePosition.Third;
        }
        else if (lowerName.Contains("4"))
        {
            position = EFieldLinePosition.Fourth;
        }

        Index = new FieldCellIndex(side, level, position);
    }
    #endregion
}

public enum ECellMarkState
{
    Inactive,
    PossibleForAction
}