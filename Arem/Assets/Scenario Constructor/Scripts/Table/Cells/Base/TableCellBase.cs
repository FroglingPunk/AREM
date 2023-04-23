using UnityEngine;

[RequireComponent(typeof(MouseHandler))]
public abstract class TableCellBase : MonoBehaviour
{
    public TableCellData Data { get; private set; }

    protected MouseHandler mouseHandler;


    public virtual void Init(TableCellData data)
    {
        Data = data;

        mouseHandler = GetComponent<MouseHandler>();
    }

    public abstract void UpdateView();
}