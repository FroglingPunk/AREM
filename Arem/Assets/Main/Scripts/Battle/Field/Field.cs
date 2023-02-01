using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    private static Field _instance;
    public static Field Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Field>();

                if(_instance == null)
                {
                    Debug.LogError("Field not found!");
                }
            }

            return _instance;
        }
    }

    private FieldCell[] _cells;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Two or more Field's in one scene!");
            Destroy(gameObject);
        }

        _cells = GetComponentsInChildren<FieldCell>();
    }


    public FieldCell this[FieldCellIndex index]
    {
        get
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].Index.Equals(index))
                {
                    return _cells[i];
                }
            }

            Debug.LogError($"Not found cell with index {index}");
            return null;
        }
    }

    public void ActionForAllCells(Action<FieldCell> action)
    {
        for (int i = 0; i < _cells.Length; i++)
            action?.Invoke(_cells[i]);
    }
}