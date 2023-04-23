using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Table Configuration", menuName = "Table/Configuration", order = 0)]
public class TableConfiguration : ScriptableObject
{
    [SerializeField] private int _cellWidth = 400;

    public int CellWidth => _cellWidth;
}