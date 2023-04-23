using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public static Table Instance { get; private set; }

    [SerializeField] private GridLayoutGroup _grid;

    [SerializeField] private EdgeTableCellBase _edgeCellPrefab;
    [SerializeField] private RowHeaderTableCellBase _rowHeaderCellPrefab;
    [SerializeField] private ColumnHeaderTableCellBase _columnHeaderCellPrefab;
    [SerializeField] private TableCellBase _contentCellPrefab;

    private List<TableCellBase> _cells = new List<TableCellBase>();
    
    public int Width { get; private set; }
    public int Height { get; private set; }


    private TimeData GetTimeData(int xID)
    {
        return _cells[xID].Data.GetContent<TimeData>();
    }

    private ActorData GetActorData(int yID)
    {
        return _cells[yID * Width].Data.GetContent<ActorData>();
    }

    public List<TimeData> GetAllTimesData()
    {
        var data = new List<TimeData>();

        for (var w = 1; w < Width; w++)
            data.Add(GetTimeData(w));

        return data;
    }

    public List<ActorData> GetAllActorsData()
    {
        var data = new List<ActorData>();

        for (var h = 1; h < Height; h++)
            data.Add(GetActorData(h));

        return data;
    }

    public ActionData GetActionData(TimeData timeData, ActorData actorData)
    {
        var cell = _cells.Find((cell) => cell.Data.GetContent<TimeData>() == timeData && cell.Data.GetContent<ActorData>() == actorData);
        return cell == null ? null : cell.Data.GetContent<ActionData>();
    }

    public TableCellAction GetTableCellAction(ActionState actionState)
    {
        var cell = _cells.Find((cell) =>
        {
            var actionData = cell.Data.GetContent<ActionData>();
            return actionData != null && actionData.States.Contains(actionState);
        });

        return cell as TableCellAction;
    }

    public TableCellData GetTableCellData(ActionState actionState)
    {
        var cell = _cells.Find((cell) =>
        {
            var actionData = cell.Data.GetContent<ActionData>();
            return actionData != null && actionData.States.Contains(actionState);
        });

        return cell == null ? null : cell.Data;
    }



    public bool TryRenameActor(ActorData renamingActor, string newActorName)
    {
        if (string.IsNullOrEmpty(newActorName))
            return false;

        var allActorsData = GetAllActorsData();
        for (var i = 0; i < allActorsData.Count; i++)
        {
            if (renamingActor == allActorsData[i])
                continue;

            if (allActorsData[i].Name == newActorName)
                return false;
        }

        renamingActor.Name = newActorName;
        UpdateCells();
        return true;
    }

    public bool TryAddActorRow(string actorName)
    {
        if (string.IsNullOrEmpty(actorName))
            return false;

        var allActorsData = GetAllActorsData();
        if (allActorsData.Find((actor) => actor.Name == actorName) != null)
            return false;

        AddRow(new ActorData(actorName));
        return true;
    }


    private void Start()
    {
        Instance = this;

        Create(Deserialize());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var dayNumber = (ushort)((Width - 1) / 3 + 1);
            var dayPart = (EDayPart)((Width - 1) % 3);
            var timeData = new TimeData(dayNumber, dayPart);

            AddColumn(timeData);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            var actorData = new ActorData((Height + 1).ToString());
            AddRow(actorData);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Serialize();
        }
    }


    private void AddCell(TableCellBase cellPrefab, TableCellData cellData)
    {
        var orderIndex = cellData.YID * Width + cellData.XID;

        var cell = Instantiate(cellPrefab, _grid.transform);
        cell.transform.SetSiblingIndex(orderIndex);
        cell.Init(cellData);

        if (_cells.Count > orderIndex)
            _cells.Insert(orderIndex, cell);
        else
            _cells.Add(cell);
    }

    private void Create()
    {
        Clear();

        int defaultWidth = 4;
        int defaultHeight = 4;

        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = defaultWidth;

        Width = defaultWidth;
        Height = defaultHeight;

        AddCell(_edgeCellPrefab, new TableCellData(0, 0));

        for (var w = 1; w < defaultWidth; w++)
        {
            var dayNumber = (ushort)((w - 1) / 3 + 1);
            var dayPart = (EDayPart)((w - 1) % 3);
            var timeData = new TimeData(dayNumber, dayPart);

            AddCell(_columnHeaderCellPrefab, new TableCellData(w, 0, timeData));
        }

        for (var h = 1; h < defaultHeight; h++)
        {
            var actorData = new ActorData(h.ToString());

            AddCell(_rowHeaderCellPrefab, new TableCellData(0, h, actorData));

            for (var w = 1; w < defaultWidth; w++)
            {
                var timeData = GetTimeData(w);
                var actionData = new ActionData();

                AddCell(_contentCellPrefab, new TableCellData(w, h, actorData, timeData, actionData));
            }
        }
    }

    private void Create(SerializableTableData tableData)
    {
        if (tableData == null)
        {
            Create();
            return;
        }

        Clear();

        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = tableData.Width;

        Width = tableData.Width;
        Height = tableData.Height;

        AddCell(_edgeCellPrefab, new TableCellData(0, 0));

        for (var w = 1; w < Width; w++)
        {
            var timeData = tableData.Times[w - 1];
            AddCell(_columnHeaderCellPrefab, new TableCellData(w, 0, timeData));
        }

        for (var h = 1; h < Height; h++)
        {
            var actorData = tableData.Actors[h - 1];

            AddCell(_rowHeaderCellPrefab, new TableCellData(0, h, actorData));

            for (var w = 1; w < Width; w++)
            {
                var timeData = GetTimeData(w);
                var actionData = BuildActionData(w, h, tableData);

                AddCell(_contentCellPrefab, new TableCellData(w, h, actorData, timeData, actionData));
            }
        }
    }

    public ActionData BuildActionData(int cellXID, int cellYID, SerializableTableData tableData)
    {
        var cellID = cellYID * Width + cellXID;
        var serializableStates = tableData.GetStatesInCell(cellID);
        var states = new List<ActionState>();

        serializableStates.ForEach((serializableAction) =>
        {
            var state = new ActionState();
            state.Description = serializableAction.Description;

            for (var i = 0; i < serializableAction.Conditions.Length; i++)
            {
                var serializableCondition = serializableAction.Conditions[i];
                var condition = new ActionCondition
                {
                    State = _cells[serializableCondition.ActionCellID].Data.GetContent<ActionData>().States[serializableCondition.ActionID]
                };
                state.Conditions.Add(condition);
            }

            states.Add(state);
        });

        return new ActionData(states);
    }

    public void Clear()
    {
        foreach (Transform child in _grid.transform)
        {
            Destroy(child.gameObject);
        }

        Width = 0;
        Height = 0;

        _cells.Clear();
    }

    public void AddColumn()
    {
        var dayNumber = (ushort)((Width - 1) / 3 + 1);
        var dayPart = (EDayPart)((Width - 1) % 3);
        var timeData = new TimeData(dayNumber, dayPart);

        AddColumn(timeData);
    }

    public void AddRow()
    {
        var actorData = new ActorData((Height + 1).ToString());
        AddRow(actorData);
    }

    public void AddColumn(TimeData timeData)
    {
        Width++;
        _grid.constraintCount = Width;

        var xID = Width - 1;

        AddCell(_columnHeaderCellPrefab, new TableCellData(xID, 0, timeData));

        for (var h = 1; h < Height; h++)
        {
            var actorData = GetActorData(h);
            var actionData = new ActionData();
            AddCell(_contentCellPrefab, new TableCellData(xID, h, timeData, actorData, actionData));
        }
    }

    public void AddRow(ActorData actorData)
    {
        Height++;

        var yID = Height - 1;

        AddCell(_rowHeaderCellPrefab, new TableCellData(0, yID, actorData));

        for (var w = 1; w < Width; w++)
        {
            var timeData = GetTimeData(w);
            var actionData = new ActionData();

            AddCell(_contentCellPrefab, new TableCellData(w, yID, actorData, timeData, actionData));
        }
    }

    public void RemoveRow(int yID)
    {
        var removedStates = new List<ActionState>();

        for (int id = yID * Width, maxID = id + Width; id < maxID; id++)
        {
            var cell = _cells[id];

            var actionData = cell.Data.GetContent<ActionData>();
            if (actionData != null)
                removedStates.AddRange(actionData.States);

            Destroy(cell.gameObject);
        }

        _cells.RemoveRange(yID * Width, Width);
        Height--;

        _cells.ForEach((cell) =>
        {
            var actionData = cell.Data.GetContent<ActionData>();
            if (actionData == null)
                return;

            for (var stateID = actionData.States.Count - 1; stateID >= 0; stateID--)
            {
                var state = actionData.States[stateID];

                for (var conditionID = state.Conditions.Count - 1; conditionID >= 0; conditionID--)
                {
                    var condition = state.Conditions[conditionID];

                    if (!removedStates.Contains(condition.State))
                        continue;

                    state.Conditions.Remove(condition);
                }
            }
        });

        UpdateCells();
    }

    public void RemoveStateInActionData(ActionState removedState)
    {
        var a = _cells[4].Data.GetContent<ActionData>().States[0];
        var b = _cells[8].Data.GetContent<ActionData>().States[0].Conditions[0].State;

        var cell = GetTableCellData(removedState);
        cell.GetContent<ActionData>().States.Remove(removedState);

        _cells.ForEach((cell) =>
        {
            var actionData = cell.Data.GetContent<ActionData>();
            if (actionData == null)
                return;

            for (var stateID = actionData.States.Count - 1; stateID >= 0; stateID--)
            {
                var state = actionData.States[stateID];

                for (var conditionID = state.Conditions.Count - 1; conditionID >= 0; conditionID--)
                {
                    var condition = state.Conditions[conditionID];

                    if (removedState != condition.State)
                        continue;

                    state.Conditions.Remove(condition);
                }
            }
        });
    }

    public void RemoveLastColumn()
    {
        for (var h = Height - 1; h >= 0; h--)
        {
            var cellID = h * Width + Width - 1;
            var cell = _cells[cellID];
            _cells.Remove(cell);
            Destroy(cell.gameObject);
        }

        Width--;
        _grid.constraintCount = Width;

        UpdateCells();
    }

    public void UpdateCells()
    {
        for (var i = 0; i < _cells.Count; i++)
        {
            var xID = i % Width;
            var yID = i / Width;

            _cells[i].Data.XID = xID;
            _cells[i].Data.YID = yID;

            _cells[i].UpdateView();
        }
        _cells.ForEach((cell) => cell.UpdateView());
    }

    public void Serialize()
    {
        var actors = new ActorData[Height - 1];

        for (var h = 1; h < Height; h++)
        {
            var cell = _cells[h * Width];
            var actorData = cell.Data.GetContent<ActorData>();
            actors[h - 1] = actorData;
        }

        var times = new TimeData[Width - 1];

        for (var w = 1; w < Width; w++)
        {
            var cell = _cells[w];
            var timeData = cell.Data.GetContent<TimeData>();
            times[w - 1] = timeData;
        }

        var states = new List<SerializableActionState>(Height * Width);

        for (var h = 1; h < Height; h++)
        {
            for (var w = 1; w < Width; w++)
            {
                var cellID = h * Width + w;
                var cell = _cells[cellID];

                var actionData = cell.Data.GetContent<ActionData>();

                for (var i = 0; i < actionData.States.Count; i++)
                {
                    var state = actionData.States[i];
                    var serializableState = new SerializableActionState();
                    serializableState.CellID = cellID;
                    serializableState.ID = i;
                    serializableState.Description = state.Description;

                    serializableState.Conditions = new SerializableActionCondition[state.Conditions.Count];

                    for (var p = 0; p < state.Conditions.Count; p++)
                    {
                        var condition = state.Conditions[p];
                        var serializableCondition = new SerializableActionCondition();
                        serializableCondition.IsActive = condition.IsActive;

                        var condtionStateTableCellData = GetTableCellData(condition.State);
                        serializableCondition.ActionCellID = condtionStateTableCellData.YID * Width + condtionStateTableCellData.XID;
                        serializableCondition.ActionID = condtionStateTableCellData.GetContent<ActionData>().States.IndexOf(condition.State);

                        serializableState.Conditions[p] = serializableCondition;
                    }

                    states.Add(serializableState);
                }
            }
        }


        var serializableTableData = new SerializableTableData();
        serializableTableData.Actors = actors;
        serializableTableData.Times = times;
        serializableTableData.ActionStates = states.ToArray();

        var json = JsonUtility.ToJson(serializableTableData);
        File.WriteAllText(SerializedTableDataPath, json);
    }

    public SerializableTableData Deserialize()
    {
        if (!File.Exists(SerializedTableDataPath))
            return null;

        var json = File.ReadAllText(SerializedTableDataPath);
        return JsonUtility.FromJson<SerializableTableData>(json);
    }

    private string SerializedTableDataPath => Path.Combine(Application.streamingAssetsPath, "ScenarioTableData.txt");
}