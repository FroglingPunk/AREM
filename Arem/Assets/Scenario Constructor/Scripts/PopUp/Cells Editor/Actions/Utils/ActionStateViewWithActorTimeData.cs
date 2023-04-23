using UnityEngine;
using UnityEngine.UI;

public class ActionStateViewWithActorTimeData : ActionStateView
{
    [SerializeField] private Text _textTime;
    [SerializeField] private Text _textActor;


    public override void Init(ActionState actionState)
    {
        base.Init(actionState);

        var cellData = Table.Instance.GetTableCellData(actionState);
        _textActor.text = cellData.GetContent<ActorData>().ToString();
        _textTime.text = cellData.GetContent<TimeData>().ToString();
    }
}